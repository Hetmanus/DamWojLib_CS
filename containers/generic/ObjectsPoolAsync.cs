//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: N/A
// Note: Pool of custom objects that can be accessed by different threads 
//
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace DamWojLib
{
    /// <summary>
    /// Pool of custom T that can be accessed by different threads 
    /// </summary>
    public class ObjectsPoolAsync<T> where T : class
    {
        private readonly Creator m_creator;
        private readonly HashSet<T> m_freeObjects = new HashSet<T>();
        private readonly object l_lock = new object();

        public ObjectsPoolAsync(Creator creator)
        {
            if (creator == null)
            {
                throw new NullReferenceException("ObjectsPoolAsync creator in ctor can't be null");
            }
            m_creator = creator;
        }

        public T Extract(object state = null)
        {
            lock (l_lock)
            {
                if (m_freeObjects.Count > 0)
                {
                    var answer = m_freeObjects.First();
                    m_freeObjects.Remove(answer);
                    return answer;
                }
            }
            return m_creator();
        }
        public bool Store(T item)
        {
            lock (l_lock)
            {
                return m_freeObjects.Add(item);
            }
        }
        public void Clear()
        {
            lock (l_lock)
            {
                m_freeObjects.Clear();
            }
        }

        public delegate T Creator();
    }
}
