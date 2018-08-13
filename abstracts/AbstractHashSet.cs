//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: 23.06.18
// Note: For faster and easier creation of custom lists
//
//----------------------------------------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;
using System;

namespace DamWojLib
{
    /// <summary>
    /// Abstract Wrapper around Syste.Collection.Generic.HashSet<T>, all basic HashSet<T> methods/properties are virtual.
    /// </summary>
    /// <typeparam name="T">Type of List Item</typeparam>
    public class AbstractHashSet<T> : ICollection<T>
    {
        private readonly HashSet<T> m_data;

        public AbstractHashSet()
        {
            m_data = new HashSet<T>();
        }
        public AbstractHashSet(IEqualityComparer<T> comparer)
        {
            m_data = new HashSet<T>(comparer);
        }
        public AbstractHashSet(IEnumerable<T> collection)
        {
            m_data = new HashSet<T>(collection);
        }
        public AbstractHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            m_data = new HashSet<T>(collection, comparer);
        }

        public virtual int Count { get { return m_data.Count; } }
        public virtual bool IsReadOnly { get { return false; } }
        public virtual IEqualityComparer<T> Comparer { get { return m_data.Comparer; } }

        public virtual void Add(T item)
        {
            m_data.Add(item);
        }
        public virtual bool Remove(T item)
        {
            return m_data.Remove(item);
        }
        public virtual void Clear()
        {
            m_data.Clear();
        }
        public virtual bool Contains(T item)
        {
            return m_data.Contains(item);
        }
        public virtual void CopyTo(T[] array, int index, int count)
        {
            m_data.CopyTo(array, index, count);
        }
        public virtual void ExceptWith(IEnumerable<T> other)
        {
            m_data.ExceptWith(other);
        }
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            m_data.GetObjectData(info, context);
        }
        public virtual void IntersectWith(IEnumerable<T> other)
        {
            m_data.IntersectWith(other);
        }
        public virtual bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return m_data.IsProperSubsetOf(other);
        }
        public virtual bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return m_data.IsProperSupersetOf(other);
        }
        public virtual bool IsSubsetOf(IEnumerable<T> other)
        {
            return m_data.IsSubsetOf(other);
        }
        public virtual bool IsSupersetOf(IEnumerable<T> other)
        {
            return m_data.IsSupersetOf(other);
        }
        public virtual void OnDeserialization(object sender)
        {
            m_data.OnDeserialization(sender);
        }
        public virtual bool Overlaps(IEnumerable<T> other)
        {
            return m_data.Overlaps(other);
        }
        public virtual int RemoveWhere(Predicate<T> predicate)
        {
            return m_data.RemoveWhere(predicate);
        }
        public virtual bool SetEquals(IEnumerable<T> other)
        {
            return m_data.SetEquals(other);
        }
        public virtual void SymmetricExceptWith(IEnumerable<T> other)
        {
            m_data.SymmetricExceptWith(other);
        }
        public virtual void TrimExcess()
        {
            m_data.TrimExcess();
        }
        public virtual void UnionWith(IEnumerable<T> other)
        {
            m_data.UnionWith(other);
        }

        public virtual HashSet<T>.Enumerator GetEnumerator()
        {
            return m_data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (this as AbstractHashSet<T>).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as AbstractHashSet<T>).GetEnumerator();
        }
        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            (this as AbstractHashSet<T>).CopyTo(array, arrayIndex, m_data.Count);
        }
    }
}