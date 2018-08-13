//-----------------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: 23.06.18
// Note: For use as Dictionary key to compare to sets of objects
//          Hash is equal to sum of content hashes, 
//          Equals returns true if given Collection have same content as this set
//
// Legend:
//
//-----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace DamWojLib
{
    /// <summary>
    /// Wrapper around Equal and GetHashCode
    /// </summary>
    public struct KeySet<T> : ICollection<T>
    {
        private readonly HashSet<T> m_data;
        private readonly int m_hashCode;

        public KeySet(HashSet<T> content)
        {
            m_data = new HashSet<T>();
            m_hashCode = 0;
            foreach (var item in content)
            {
                m_data.Add(item);
                m_hashCode += item.GetHashCode();
            }
        }
        public int Count
        {
            get { return m_data.Count; }
        }
        public bool Contains(T item)
        {
            return m_data.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            m_data.CopyTo(array, arrayIndex);
        }
        public override bool Equals(object obj)
        {
            if (obj is ICollection<T> && (obj as ICollection<T>).Count == m_data.Count)
            {
                foreach (var item in obj as IEnumerable<T>)
                {
                    if (!m_data.Contains(item))
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (obj is ICollection && (obj as ICollection).Count == m_data.Count)
            {
                foreach (var item in obj as IEnumerable)
                {
                    if (!(item is T) || !m_data.Contains((T)item))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return m_hashCode;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return m_data.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_data.GetEnumerator();
        }
        bool ICollection<T>.IsReadOnly
        {
            get { return true; }
        }
        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }
        void ICollection<T>.Clear()
        {
            throw new NotSupportedException("Collection is read-only.");
        }
        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException("Collection is read-only.");
        }
    }
}