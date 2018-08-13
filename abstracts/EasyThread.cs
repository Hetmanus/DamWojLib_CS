//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: N/A
// Note: Easier way of starting looping threats
//
//----------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Threading;

namespace DamWojLib
{
    public abstract class EasyThread : IDisposable
    {
        volatile bool m_dispose = false;
        volatile int m_deltaTime = 0;
        protected volatile int cycleSleepTime;

        readonly TimeSpan? m_disposeTimeOut;
        readonly Thread m_thread = null;
        readonly Stopwatch m_watch = new Stopwatch();
        readonly object mainLock = new object();

        public EasyThread() : this(100)
        {

        }
        public EasyThread(int cycleSleepTime, TimeSpan? disposeTimeOut = null)
        {
            this.cycleSleepTime = cycleSleepTime;
            this.m_disposeTimeOut = disposeTimeOut;
            this.m_thread = new Thread(Loop);
            this.m_thread.Start();
        }

        public int deltaTime { get { return m_deltaTime; }}

        void Loop()
        {
            while (!m_dispose)
            {
                lock (mainLock)
                {
                    m_watch.Start();
                    Update();
                    m_watch.Stop();
                }
                if (m_watch.ElapsedMilliseconds > cycleSleepTime)
                {
                    m_deltaTime = (int)m_watch.ElapsedMilliseconds;
                }
                else
                {
                    Thread.Sleep(cycleSleepTime - (int)m_watch.ElapsedMilliseconds);
                    m_deltaTime = cycleSleepTime;
                }
                m_watch.Reset();
            }
        }
        /// <summary>
        /// Called in loop
        /// </summary>
        protected abstract void Update();

        /// <summary>
        /// Use Dispose to stop thread
        /// </summary>
        protected virtual void Dispose()
        {
            m_dispose = true;
            if (m_disposeTimeOut != null)
            {
                m_thread.Join(m_disposeTimeOut.Value);
            }
            else
            {
                m_thread.Join();
            }
        }
        /// <summary>
        /// Use Dispose to stop thread
        /// </summary>
        void IDisposable.Dispose()
        {
            (this as EasyThread).Dispose();
        }
    }
}
