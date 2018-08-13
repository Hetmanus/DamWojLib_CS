//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: N/A
// Note: HashSet with support for index seter and geter
//
//----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace DamWojLib
{
    /// <summary>
    /// HashSet with support for index seter and geter
    /// </summary>
    [Serializable]
    public class HashSetIndexed<T> : IList<T>
    {
        readonly HashSet<T> m_set = new HashSet<T>();
        readonly List<T> m_list = new List<T>();

        public HashSetIndexed()
        {

        }
        public HashSetIndexed(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                m_set.Add(item);
            }
            foreach (var item in m_set)
            {
                m_list.Add(item);
            }
        }

        public T this[int index]
        {
            get
            {
                return m_list[index];
            }
            set
            {
                var oldValue = m_list[index];
                m_set.Remove(oldValue);
                if (m_set.Add(value))
                {
                    m_list[index] = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("Value can't be inserted in to HashSetIndexed[{0}], becouse it already exists within this set", index));
                }
            }
        }
        public int Count
        {
            get { return m_list.Count; }
        }
        public bool IsReadOnly
        {
            get { return (m_list as IList<T>).IsReadOnly; }
        }

        public HashSetIndexed(HashSet<T> m_set, List<T> m_list)
        {
            this.m_set = m_set;
            this.m_list = m_list;
        }

        public int IndexOf(T item)
        {
            return m_list.IndexOf(item);
        }
        public void Insert(int index, T item)
        {
            if (m_set.Add(item))
            {
                m_list.Insert(index, item);
            }
            else
            {
                throw new ArgumentException("Value can't be inserted in to HashSetIndexed, becouse it already exists within this set");
            }
        }
        public void RemoveAt(int index)
        {
            m_set.Remove(m_list[index]);
            m_list.RemoveAt(index);
        }
        public void Add(T item)
        {
            if (m_set.Add(item))
            {
                m_list.Add(item);
            }
            else
            {
                throw new ArgumentException("Value can't be added in to HashSetIndexed, becouse it already exists within this set");
            }
        }
        public void Clear()
        {
            m_set.Clear();
            m_list.Clear();
        }
        public bool Contains(T item)
        {
            return m_set.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            m_list.CopyTo(array, arrayIndex);
        }
        public bool Remove(T item)
        {
            return Remove(item, true);
        }
        /// <summary>
        /// Removes item from collection
        /// </summary>
        /// <param name="removeIndex"> false -> container on index containing removed item will be set to default(T) </param>
        /// <returns></returns>
        public bool Remove(T item, bool removeIndex)
        {
            if (m_set.Remove(item))
            {
                if (removeIndex)
                {
                    return m_list.Remove(item);
                }
                else
                {
                    m_list[IndexOf(item)] = default(T);
                }
            }
            return false;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return m_list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_list.GetEnumerator();
        }
    }
}
