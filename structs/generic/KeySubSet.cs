//-----------------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: 23.06.18
// Note: For use as Dictionary key to compare to sets of objects
//          Hash is equal to hash of its generic type T, 
//          Equals returns true if this is subset of given Collection
//          Use of this as key will decrease Dict efficiency    
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
    public struct KeySuperSet<T> : ICollection<T>
    {
        private readonly HashSet<T> m_data;

        public KeySuperSet(HashSet<T> content)
        {
            m_data = new HashSet<T>();
            foreach (var item in content)
            {
                m_data.Add(item);
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
            if (obj is IEnumerable<T>)
            {
                return m_data.IsSupersetOf(obj as IEnumerable<T>);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return typeof(T).GetHashCode();
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