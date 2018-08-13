//-----------------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: 23.06.18
// Note: For use as Dictionary key to compare to collections of objects
//          Hash is equal to sum of content hashes, 
//          Equals returns true if given Collection have same content on same positions as this collection
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
    public struct KeyCollection<T> : ICollection<T>
    {
        private readonly ICollection<T> m_data;
        private readonly int m_hashCode;

        public KeyCollection(HashSet<T> content)
        {
            m_data = new List<T>();
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
            if ((obj is ICollection<T> && (obj as ICollection<T>).Count == m_data.Count)
                || (obj is ICollection && (obj as ICollection).Count == m_data.Count))
            {
                var thisEnum = (m_data as IEnumerable).GetEnumerator();
                var objEnum = (obj as IEnumerable).GetEnumerator();
                while (thisEnum.MoveNext() && objEnum.MoveNext())
                {
                    if (!thisEnum.Current.Equals(objEnum.Current))
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