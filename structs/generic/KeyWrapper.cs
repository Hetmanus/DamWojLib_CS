using System;
using System.Collections;
using System.Collections.Generic;
using DamWojLib.EnumeratorExtensions;

public struct KeyWrappe
{
    private readonly Func<T> m_data;
    private readonly int m_hashCode;

    public KeyWrapper(HashSet<T> content)
    {
        m_data = new HashSet<T>();
        m_hashCode = 0;
        foreach (var item in content)
        {
            m_data.Add(item);
            m_hashCode += item.GetHashCode();
        }
    }

    public override bool Equals(object obj)
    {
        if(obj is KeySet<T>)
        {
            obj = ((KeySet<T>)obj).m_data;
        }
        //if (obj is IEnumerator)
        //{
        //    IEnumerator enumerator = obj as IEnumerator;
        //    while (enumerator.MoveNext())
        //    {
        //        if (!(enumerator.Current is T) || !m_data.Contains((T)enumerator.Current))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        if (obj is IEnumerable)
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
}