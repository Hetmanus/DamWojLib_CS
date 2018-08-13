//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: 23.06.18
// Note: For faster and easier creation of custom lists
//
//----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace DamWojLib
{
    /// <summary>
    /// Abstract Wrapper around Syste.Collection.Generic.List<T>, all basic List<T> methods/properties are virtual.
    /// </summary>
    /// <typeparam name="T">Type of List Item</typeparam>
    [Serializable]
    public abstract class AbstractList<T> : IList<T>
    {
        private readonly List<T> m_data;
        //protected List<T> data { get { return m_data; } }

        public AbstractList()
        {
            this.m_data = new List<T>();
        }
        public AbstractList(IEnumerable<T> collection)
        {
            this.m_data = new List<T>(collection);
        }
        public AbstractList(int capacity)
        {
            this.m_data = new List<T>(capacity);
        }

        #region public
        public virtual int Count
        {
            get { return (m_data as IList<T>).Count; }
        }
        public virtual int Capacity
        {
            get
            {
                return m_data.Capacity;
            }
            set
            {
                m_data.Capacity = value;
            }
        }
        public virtual bool IsReadOnly
        {
            get { return (m_data as IList<T>).IsReadOnly; }
        }

        public virtual int IndexOf(T item)
        {
            return (m_data as IList<T>).IndexOf(item);
        }
        public virtual void Insert(int index, T item)
        {
            (m_data as IList<T>).Insert(index, item);
        }
        public virtual void RemoveAt(int index)
        {
            (m_data as IList<T>).RemoveAt(index);
        }
        public virtual void RemoveAll(Predicate<T> predicate)
        {
            m_data.RemoveAll(predicate);
        }
        public virtual void RemoveRange(int startIndex, int count)
        {
            m_data.RemoveRange(startIndex, count);
        }
        public virtual T this[int index]
        {
            get
            {
                return (m_data as IList<T>)[index];
            }
            set
            {
                (m_data as IList<T>)[index] = value;
            }
        }
        public virtual void Add(T item)
        {
            (m_data as IList<T>).Add(item);
        }
        public virtual void Clear()
        {
            (m_data as IList<T>).Clear();
        }
        public virtual bool Contains(T item)
        {
            return (m_data as IList<T>).Contains(item);
        }
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            (m_data as IList<T>).CopyTo(array, arrayIndex);
        }
        public virtual bool Remove(T item)
        {
            return (m_data as IList<T>).Remove(item);
        }
        public virtual List<T>.Enumerator GetEnumerator()
        {
            return m_data.GetEnumerator();
        }
        #endregion
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (this as AbstractList<T>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }
    }
}
