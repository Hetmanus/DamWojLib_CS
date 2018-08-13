//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: N/A
//
//----------------------------------------------------------------------------

using System;

namespace DamWojLib
{
    /// <summary>
    /// Inplements Singleton functinality
    /// </summary>
    /// <typeparam name="T">Must be Declaring class type</typeparam>
    public abstract class Singleton<T> : IDisposable where T : Singleton<T>, new()
    {
        static T s_instance;
        public static T singleton { get { return s_instance != null ? s_instance : InstantiateSingleton(); } }
        /// <summary>
        /// Use this method to create singelton instance using ctor with no parameters
        /// </summary>
        public static T InstantiateSingleton()
        {
            if (s_instance == null)
            {
                s_instance = new T();
            }
            return s_instance;
        }
        /// <summary>
        /// Use this method to create singelton instance using ctor with parameters
        /// </summary>
        public static T InstantiateSingleton(params object[] ctorParameters)
        {
            if (s_instance == null)
            {
                s_instance = (T)Activator.CreateInstance(typeof(T), ctorParameters);
            }
            return s_instance;
        }
        /// <summary>
        /// Removes old singleton instance so that new can be created
        /// </summary>
        public static bool Dispose()
        {
            if(s_instance != null)
            {
                s_instance = null;
                return true;
            }
            return false;
        }

        public Singleton()
        {
            if (s_instance != null)
            {
                throw new NotSupportedException("Duplicated Singleton instance, dispose of old instance to create new one");
            }
            if (typeof(T) != GetType().DeclaringType)
            {
                throw new NotSupportedException(string.Format("Singleton inheriting class {0} recived {1} type as its Generic type", this.GetType().DeclaringType.FullName, typeof(T).FullName));
            }
        }

        void IDisposable.Dispose()
        {
            if (s_instance == this)
            {
                s_instance = null;
            }
        }
    }
}
