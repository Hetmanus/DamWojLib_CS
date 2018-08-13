//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: N/A
// Note: Easier way of creating and handling internet connection
//
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DamWojLib.EnumeratorExtensions;

namespace DamWojLib
{
    /// <summary>
    /// Inherit from this class to create udp socket
    /// </summary>
    public abstract class SocketUDP : IDisposable
    {
        Action<EndPoint, IEnumerable<byte>> m_eSend;
        object m_eSendLock = new object();
        Action<EndPoint, IEnumerable<byte>> m_eRecive;
        object m_eReciveLock = new object();

        public event Action<EndPoint, IEnumerable<byte>> eSend
        {
            add
            {
                lock (m_eSendLock)
                {
                    m_eSend += value;
                }
            }
            remove
            {
                lock (m_eSendLock)
                {
                    m_eSend -= value;
                }
            }
        }
        public event Action<EndPoint, IEnumerable<byte>> eRecive
        {
            add
            {
                lock (m_eReciveLock)
                {
                    m_eRecive += value;
                }
            }
            remove
            {
                lock (m_eReciveLock)
                {
                    m_eRecive -= value;
                }
            }
        }

        ObjectsPoolAsync<byte[]> m_sendBuffers;
        ObjectsPoolAsync<byte[]> m_reciveBuffers;
        Socket m_socket;
        IPEndPoint m_bindPoint;
        volatile bool m_disposing = false;

        Timer m_timer;
        volatile uint m_timeSinceLastSend = 0;
        volatile uint m_timeSinceLastRecive = 0;

        public SocketUDP(IPAddress ip, ushort port, int sendBufferSize, int receiveBufferSize, uint timerFrequency = 10)
        {
            if (ip == null)
            {
                ip = IPAddress.Any;
            }
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            m_sendBuffers = new ObjectsPoolAsync<byte[]>(() => new byte[sendBufferSize]);
            m_socket.SendBufferSize = sendBufferSize;
            m_reciveBuffers = new ObjectsPoolAsync<byte[]>(() => new byte[receiveBufferSize]);
            m_socket.ReceiveBufferSize = receiveBufferSize;

            #region fixes problem of connecting back to self causing socket to throw exception and stop working
            int SIO_UDP_CONNRESET = -1744830452;
            byte[] inValue = new byte[] { 0 };
            byte[] outValue = new byte[] { 0 };
            m_socket.IOControl(SIO_UDP_CONNRESET, inValue, outValue);
            #endregion

            m_bindPoint = new IPEndPoint(ip, port);
            m_socket.Bind(m_bindPoint);

            //counting time since last updates
            uint newValue = 0;
            m_timer = new Timer(o =>
            {
                newValue = m_timeSinceLastSend + timerFrequency;
                if(newValue > m_timeSinceLastSend)
                {
                    m_timeSinceLastSend = newValue;
                }
                else
                {
                    m_timeSinceLastSend = uint.MaxValue;
                }

                newValue = m_timeSinceLastRecive + timerFrequency;
                if (newValue > m_timeSinceLastRecive)
                {
                    m_timeSinceLastRecive = newValue;
                }
                else
                {
                    m_timeSinceLastRecive = uint.MaxValue;
                }
            }, null, timerFrequency, timerFrequency);
        }
        public SocketUDP(IPAddress ip, ushort port, int bufferSize, uint timerFrequency = 10)
            : this(ip, port, bufferSize, bufferSize, timerFrequency) { }
        void IDisposable.Dispose()
        {
            ThrowIfDisposing();
            m_disposing = true;
            m_timer.Dispose();
            m_socket.Shutdown(SocketShutdown.Both);
            //wait for socket to recive and send all pending messages
            Thread.Sleep(100);
            m_socket.Close();
            //waiting for socket to close all processes
            Thread.Sleep(10);
        }

        public EndPoint localEndPoint { get { ThrowIfDisposing(); return m_socket.LocalEndPoint; } }
        public ushort port { get { ThrowIfDisposing(); return (ushort)(localEndPoint as IPEndPoint).Port; } }
        public int sendBufferSize { get { return m_socket.SendBufferSize; } }
        public int receiveBufferSize { get { return m_socket.ReceiveBufferSize; } }
        public uint timeSinceLastRecive { get { return m_timeSinceLastRecive; } }
        public uint timeSinceLastSend { get { return m_timeSinceLastSend; } }

        void reciveCallback(IAsyncResult ar)
        {
            ReciveState state = (ReciveState)ar.AsyncState;
            EndPoint source = m_bindPoint;
            int reciveSize = 0;
            try
            {
                reciveSize = m_socket.EndReceiveFrom(ar, ref source);
            }
            catch (ObjectDisposedException e)
            {
                if (e.ObjectName != "System.Net.Sockets.Socket")
                {
                    throw e;
                }
                else
                {
                    return;
                }
            }
            catch (SocketException e)
            {
                switch (e.SocketErrorCode)
                {
                    default: throw e;
                }
            }

            try
            {
                lock (m_eReciveLock)
                {
                    if (m_eRecive != null)
                    {
                        m_eRecive(source, state.buffer);
                    }
                }
                using (IEnumerator<byte> data = state.buffer.GetEnumerator(0, reciveSize))
                {
                    m_timeSinceLastRecive = 0;
                    OnReciveFrom(source, data, state.flag);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                m_reciveBuffers.Store(state.buffer);
            }
        }
        void sendCallback(IAsyncResult ar)
        {
            SendState state = (SendState)ar.AsyncState;
            int sendSize = m_socket.EndSendTo(ar);

            try
            {
                using (IEnumerator<byte> data = state.buffer.GetEnumerator(0, sendSize))
                {
                    m_timeSinceLastSend = 0;
                    OnSendTo(state.target, data, state.flag);
                }
                lock (m_eSendLock)
                {
                    if (m_eSend != null)
                    {
                        m_eSend(state.target, state.buffer);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                m_sendBuffers.Store(state.buffer);
            }
        }

        /// <summary>
        /// Begins thread responsible for reciving data
        /// </summary>
        /// <param name="flag"> flag type for recived data </param>
        public virtual void BeginRecive(SocketFlags flag = SocketFlags.None)
        {
            ThrowIfDisposing();
            byte[] buffor = m_reciveBuffers.Extract();
            EndPoint bindPoint = m_bindPoint;
            m_socket.BeginReceiveFrom(buffor, 0, buffor.Length, flag,
                ref bindPoint, reciveCallback, new ReciveState(buffor, flag));

        }
        /// <summary>
        /// Begins thread responsible for sending given data
        /// </summary>
        /// <param name="target">UDP protocol send target</param>
        /// <param name="data">Data To be Send</param>
        /// <param name="flag">Flag to be used when sending data</param>
        /// <returns>Did method manage to fit data in to buffer</returns>
        public virtual bool BeginSendTo(EndPoint target, IEnumerator<byte> data, SocketFlags flag)
        {
            ThrowIfDisposing();
            byte[] buffor = m_sendBuffers.Extract();
            int sendSize = 0;
            if(data == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                sendSize = data.ToArrayNonAloc(ref buffor, 0);
            }
            m_socket.BeginSendTo(buffor, 0, sendSize, flag, target, sendCallback, new SendState(buffor, target, flag));
            return true;
        }

        protected virtual void OnSendTo(EndPoint target, IEnumerator<byte> data, SocketFlags flag) { }
        protected virtual void OnReciveFrom(EndPoint source, IEnumerator<byte> data, SocketFlags flag) { }

        public override string ToString()
        {
            ThrowIfDisposing();
            return string.Format("Socket(AdrFam={0}, Soc={1}, Proto={2}, Port={3}, SenBufSize={4}, RecBufSize={5})",
                m_socket.AddressFamily, m_socket.SocketType, m_socket.ProtocolType, port, sendBufferSize, receiveBufferSize);
        }

        protected void ThrowIfDisposing()
        {
            if (m_disposing)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }

        struct SendState
        {
            public readonly byte[] buffer;
            public readonly EndPoint target;
            public readonly SocketFlags flag;
            public SendState(byte[] buffer, EndPoint target, System.Net.Sockets.SocketFlags flag)
            {
                this.buffer = buffer;
                this.target = target;
                this.flag = flag;
            }
        }
        struct ReciveState
        {
            public readonly byte[] buffer;
            public readonly SocketFlags flag;
            public ReciveState(byte[] buffer, System.Net.Sockets.SocketFlags flag)
            {
                this.buffer = buffer;
                this.flag = flag;
            }
        }
    }
}
