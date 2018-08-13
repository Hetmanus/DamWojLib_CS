//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: N/A
// Note: If addConditiona is not fullfiled new item will not be added
//          If any of already contained item inheriting from IComparable or IComparable<T> returns 0 when compared to newly added it will not be added
//
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DamWojLib
{
    /// <summary>
    /// List that will not allow new items if any of currentyl included items returns 0 on IComparable.Compare
    /// </summary>
    /// <typeparam name="T"> Content type, must inherit from IComparable</typeparam>
    [Serializable]
    public class ConditionalList<T> : AbstractList<T>
    {
        public Predicate<T> addCondition = null;

        public ConditionalList(Predicate<T> addCondition = null) : base()
        {
            this.addCondition = addCondition;
        }
        public ConditionalList(IEnumerable<T> collection, Predicate<T> addCondition = null)
        {
            this.addCondition = addCondition;
            foreach (var item in collection)
            {
                if(!TryAdd(item))
                {
                    throw new ArgumentException("Item Could not be added");
                }
            }
        }
        public ConditionalList(int capacity, Predicate<T> addCondition = null) : base(capacity)
        {
            this.addCondition = addCondition;
        }

        public virtual bool TryAdd(T item)
        {
            if (addCondition == null || addCondition(item))
            {
                foreach (var compare in this)
                {
                    if (compare is IComparable && (compare as IComparable).CompareTo(item) == 0)
                    {
                        return false;
                    }
                    if (compare is IComparable<T> && (compare as IComparable<T>).CompareTo(item) == 0)
                    {
                        return false;
                    }
                }
                base.Add(item);
                return true;
            }
            return false;
        }
        public virtual bool TryInsert(int index, T item)
        {
            if (addCondition == null || addCondition(item))
            {
                foreach (var compare in this)
                {
                    if (compare is IComparable && (compare as IComparable).CompareTo(item) == 0)
                    {
                        return false;
                    }
                    if (compare is IComparable<T> && (compare as IComparable<T>).CompareTo(item) == 0)
                    {
                        return false;
                    }
                }
                base.Insert(index, item);
                return true;
            }
            return false;
        }

        public sealed override void Add(T item)
        {
            TryAdd(item);
        }
        public sealed override void Insert(int index, T item)
        {
            TryInsert(index, item);
        }

        public delegate void OnRefuse(T refusingObject, T refusedObject);
    }
}
