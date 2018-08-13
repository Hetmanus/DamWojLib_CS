//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: N/A
// Note: Easier way to store multiple objects together
//
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DamWojLib
{
    [Serializable]
    public struct TupleStruct<T1, T2>
    {
        public T1 Item1;
        public T2 Item2;
        public TupleStruct(T1 value1, T2 value2)
        {
            Item1 = value1;
            Item2 = value2;
        }
        public TupleStruct(KeyValuePair<T1, T2> data)
        {
            Item1 = data.Key;
            Item2 = data.Value;
        }

        public static bool operator ==(TupleStruct<T1, T2> x, TupleProtected<T1, T2> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals((T1)(y.GetType().GetField("Item1", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item2.Equals((T2)(y.GetType().GetField("Item2", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            return true;
        }
        public static bool operator !=(TupleStruct<T1, T2> x, TupleProtected<T1, T2> y)
        {
            return !(x == y);
        }
        public static bool operator ==(TupleStruct<T1, T2> x, TupleReadOnly<T1, T2> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals(y.Item1)))
                return false;
            if (!(x.Item2.Equals(y.Item2)))
                return false;
            return true;
        }
        public static bool operator !=(TupleStruct<T1, T2> x, TupleReadOnly<T1, T2> y)
        {
            return !(x == y);
        }

        public static implicit operator Tuple<T1, T2>(TupleStruct<T1, T2> tupl)
        {
            return new Tuple<T1, T2>(tupl.Item1, tupl.Item2);
        }
        public static implicit operator TupleReadOnly<T1, T2>(TupleStruct<T1, T2> tupl)
        {
            return new TupleReadOnly<T1, T2>(tupl.Item1, tupl.Item2);
        }
        public static implicit operator KeyValuePair<T1, T2>(TupleStruct<T1, T2> tupl)
        {
            return new KeyValuePair<T1, T2>(tupl.Item1, tupl.Item2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    [Serializable]
    public struct TupleStruct<T1, T2, T3>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public TupleStruct(T1 value1, T2 value2, T3 value3)
        {
            Item1 = value1;
            Item2 = value2;
            Item3 = value3;
        }
        public TupleStruct(KeyValuePair<T1, KeyValuePair<T2, T3>> data)
        {
            Item1 = data.Key;
            Item2 = data.Value.Key;
            Item3 = data.Value.Value;
        }

        public static bool operator ==(TupleStruct<T1, T2, T3> x, TupleProtected<T1, T2, T3> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals((T1)(y.GetType().GetField("Item1", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item2.Equals((T2)(y.GetType().GetField("Item2", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item3.Equals((T3)(y.GetType().GetField("Item3", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            return true;
        }
        public static bool operator !=(TupleStruct<T1, T2, T3> x, TupleProtected<T1, T2, T3> y)
        {
            return !(x == y);
        }
        public static bool operator ==(TupleStruct<T1, T2, T3> x, TupleReadOnly<T1, T2, T3> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals(y.Item1)))
                return false;
            if (!(x.Item2.Equals(y.Item2)))
                return false;
            if (!(x.Item3.Equals(y.Item3)))
                return false;
            return true;
        }
        public static bool operator !=(TupleStruct<T1, T2, T3> x, TupleReadOnly<T1, T2, T3> y)
        {
            return !(x == y);
        }

        public static implicit operator Tuple<T1, T2, T3>(TupleStruct<T1, T2, T3> tupl)
        {
            return new Tuple<T1, T2, T3>(tupl.Item1, tupl.Item2, tupl.Item3);
        }
        public static implicit operator TupleReadOnly<T1, T2, T3>(TupleStruct<T1, T2, T3> tupl)
        {
            return new TupleReadOnly<T1, T2, T3>(tupl.Item1, tupl.Item2, tupl.Item3);
        }
        public static implicit operator KeyValuePair<T1, KeyValuePair<T2, T3>>(TupleStruct<T1, T2, T3> tupl)
        {
            return new KeyValuePair<T1, KeyValuePair<T2, T3>>(tupl.Item1, new KeyValuePair<T2, T3>(tupl.Item2, tupl.Item3));
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().IsSubclassOf(typeof(TupleProtected<T1, T2, T3>)))
            {
                return this == (TupleProtected<T1, T2, T3>)obj;
            }
            else if (obj.GetType().IsSubclassOf(typeof(TupleReadOnly<T1, T2, T3>)))
            {
                return this == (TupleReadOnly<T1, T2, T3>)obj;
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    [Serializable]
    public struct TupleStruct<T1, T2, T3, T4>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public TupleStruct(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            Item1 = value1;
            Item2 = value2;
            Item3 = value3;
            Item4 = value4;
        }
        public TupleStruct(KeyValuePair<T1, KeyValuePair<T2, KeyValuePair<T3, T4>>> data)
        {
            Item1 = data.Key;
            Item2 = data.Value.Key;
            Item3 = data.Value.Value.Key;
            Item4 = data.Value.Value.Value;
        }

        public static bool operator ==(TupleStruct<T1, T2, T3, T4> x, TupleProtected<T1, T2, T3, T4> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals((T1)(y.GetType().GetField("Item1", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item2.Equals((T2)(y.GetType().GetField("Item2", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item3.Equals((T3)(y.GetType().GetField("Item3", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item4.Equals((T4)(y.GetType().GetField("Item4", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            return true;
        }
        public static bool operator !=(TupleStruct<T1, T2, T3, T4> x, TupleProtected<T1, T2, T3, T4> y)
        {
            return !(x == y);
        }
        public static bool operator ==(TupleStruct<T1, T2, T3, T4> x, TupleReadOnly<T1, T2, T3, T4> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals(y.Item1)))
                return false;
            if (!(x.Item2.Equals(y.Item2)))
                return false;
            if (!(x.Item3.Equals(y.Item3)))
                return false;
            if (!(x.Item4.Equals(y.Item4)))
                return false;
            return true;
        }
        public static bool operator !=(TupleStruct<T1, T2, T3, T4> x, TupleReadOnly<T1, T2, T3, T4> y)
        {
            return !(x == y);
        }

        public static implicit operator Tuple<T1, T2, T3, T4>(TupleStruct<T1, T2, T3, T4> tupl)
        {
            return new Tuple<T1, T2, T3, T4>(tupl.Item1, tupl.Item2, tupl.Item3, tupl.Item4);
        }
        public static implicit operator TupleReadOnly<T1, T2, T3, T4>(TupleStruct<T1, T2, T3, T4> tupl)
        {
            return new TupleReadOnly<T1, T2, T3, T4>(tupl.Item1, tupl.Item2, tupl.Item3, tupl.Item4);
        }
        public static implicit operator KeyValuePair<T1, KeyValuePair<T2, KeyValuePair<T3, T4>>>(TupleStruct<T1, T2, T3, T4> tupl)
        {
            return new KeyValuePair<T1, KeyValuePair<T2, KeyValuePair<T3, T4>>>
                (tupl.Item1, new KeyValuePair<T2, KeyValuePair<T3, T4>>(tupl.Item2, new KeyValuePair<T3, T4>(tupl.Item3, tupl.Item4)));
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().IsSubclassOf(typeof(TupleProtected<T1, T2, T3, T4>)))
            {
                return this == (TupleProtected<T1, T2, T3, T4>)obj;
            }
            else if (obj.GetType().IsSubclassOf(typeof(TupleReadOnly<T1, T2, T3, T4>)))
            {
                return this == (TupleReadOnly<T1, T2, T3, T4>)obj;
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    [Serializable]
    public struct TupleReadOnly<T1, T2>
    {
        public readonly T1 Item1;
        public readonly T2 Item2;
        public TupleReadOnly(T1 value1, T2 value2)
        {
            Item1 = value1;
            Item2 = value2;
        }
        public TupleReadOnly(KeyValuePair<T1, T2> data)
        {
            Item1 = data.Key;
            Item2 = data.Value;
        }

        public static bool operator ==(TupleReadOnly<T1, T2> x, TupleProtected<T1, T2> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals((T1)(y.GetType().GetField("Item1", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item2.Equals((T2)(y.GetType().GetField("Item2", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            return true;
        }
        public static bool operator !=(TupleReadOnly<T1, T2> x, TupleProtected<T1, T2> y)
        {
            return !(x == y);
        }
        public static bool operator ==(TupleReadOnly<T1, T2> x, TupleStruct<T1, T2> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals(y.Item1)))
                return false;
            if (!(x.Item2.Equals(y.Item2)))
                return false;
            return true;
        }
        public static bool operator !=(TupleReadOnly<T1, T2> x, TupleStruct<T1, T2> y)
        {
            return !(x == y);
        }

        public static implicit operator Tuple<T1, T2>(TupleReadOnly<T1, T2> tupl)
        {
            return new Tuple<T1, T2>(tupl.Item1, tupl.Item2);
        }
        public static implicit operator TupleStruct<T1, T2>(TupleReadOnly<T1, T2> tupl)
        {
            return new TupleStruct<T1, T2>(tupl.Item1, tupl.Item2);
        }
        public static implicit operator KeyValuePair<T1, T2>(TupleReadOnly<T1, T2> tupl)
        {
            return new TupleReadOnly<T1, T2>(tupl.Item1, tupl.Item2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    [Serializable]
    public struct TupleReadOnly<T1, T2, T3>
    {
        public readonly T1 Item1;
        public readonly T2 Item2;
        public readonly T3 Item3;
        public TupleReadOnly(T1 value1, T2 value2, T3 value3)
        {
            Item1 = value1;
            Item2 = value2;
            Item3 = value3;
        }
        public TupleReadOnly(KeyValuePair<T1, KeyValuePair<T2, T3>> data)
        {
            Item1 = data.Key;
            Item2 = data.Value.Key;
            Item3 = data.Value.Value;
        }


        public static bool operator ==(TupleReadOnly<T1, T2, T3> x, TupleProtected<T1, T2, T3> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals((T1)(y.GetType().GetField("Item1", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item2.Equals((T2)(y.GetType().GetField("Item2", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item3.Equals((T3)(y.GetType().GetField("Item3", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            return true;
        }
        public static bool operator !=(TupleReadOnly<T1, T2, T3> x, TupleProtected<T1, T2, T3> y)
        {
            return !(x == y);
        }
        public static bool operator ==(TupleReadOnly<T1, T2, T3> x, TupleStruct<T1, T2, T3> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals(y.Item1)))
                return false;
            if (!(x.Item2.Equals(y.Item2)))
                return false;
            if (!(x.Item3.Equals(y.Item3)))
                return false;
            return true;
        }
        public static bool operator !=(TupleReadOnly<T1, T2, T3> x, TupleStruct<T1, T2, T3> y)
        {
            return !(x == y);
        }

        public static implicit operator Tuple<T1, T2, T3>(TupleReadOnly<T1, T2, T3> tupl)
        {
            return new Tuple<T1, T2, T3>(tupl.Item1, tupl.Item2, tupl.Item3);
        }
        public static implicit operator TupleStruct<T1, T2, T3>(TupleReadOnly<T1, T2, T3> tupl)
        {
            return new TupleStruct<T1, T2, T3>(tupl.Item1, tupl.Item2, tupl.Item3);
        }
        public static implicit operator KeyValuePair<T1, KeyValuePair<T2, T3>>(TupleReadOnly<T1, T2, T3> tupl)
        {
            return new KeyValuePair<T1, KeyValuePair<T2, T3>>(tupl.Item1, new KeyValuePair<T2, T3>(tupl.Item2, tupl.Item3));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    [Serializable]
    public struct TupleReadOnly<T1, T2, T3, T4>
    {
        public readonly T1 Item1;
        public readonly T2 Item2;
        public readonly T3 Item3;
        public readonly T4 Item4;
        public TupleReadOnly(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            Item1 = value1;
            Item2 = value2;
            Item3 = value3;
            Item4 = value4;
        }
        public TupleReadOnly(KeyValuePair<T1, KeyValuePair<T2, KeyValuePair<T3, T4>>> data)
        {
            Item1 = data.Key;
            Item2 = data.Value.Key;
            Item3 = data.Value.Value.Key;
            Item4 = data.Value.Value.Value;
        }

        public static bool operator ==(TupleReadOnly<T1, T2, T3, T4> x, TupleProtected<T1, T2, T3, T4> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals((T1)(y.GetType().GetField("Item1", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item2.Equals((T2)(y.GetType().GetField("Item2", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item3.Equals((T3)(y.GetType().GetField("Item3", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            if (!(x.Item4.Equals((T4)(y.GetType().GetField("Item4", (System.Reflection.BindingFlags)36).GetValue(y)))))
                return false;
            return true;
        }
        public static bool operator !=(TupleReadOnly<T1, T2, T3, T4> x, TupleProtected<T1, T2, T3, T4> y)
        {
            return !(x == y);
        }
        public static bool operator ==(TupleReadOnly<T1, T2, T3, T4> x, TupleStruct<T1, T2, T3, T4> y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return ReferenceEquals(x, null);

            if (!(x.Item1.Equals(y.Item1)))
                return false;
            if (!(x.Item2.Equals(y.Item2)))
                return false;
            if (!(x.Item3.Equals(y.Item3)))
                return false;
            if (!(x.Item4.Equals(y.Item4)))
                return false;
            return true;
        }
        public static bool operator !=(TupleReadOnly<T1, T2, T3, T4> x, TupleStruct<T1, T2, T3, T4> y)
        {
            return !(x == y);
        }

        public static implicit operator Tuple<T1, T2, T3, T4>(TupleReadOnly<T1, T2, T3, T4> tupl)
        {
            return new Tuple<T1, T2, T3, T4>(tupl.Item1, tupl.Item2, tupl.Item3, tupl.Item4);
        }
        public static implicit operator TupleStruct<T1, T2, T3, T4>(TupleReadOnly<T1, T2, T3, T4> tupl)
        {
            return new TupleStruct<T1, T2, T3, T4>(tupl.Item1, tupl.Item2, tupl.Item3, tupl.Item4);
        }
        public static implicit operator KeyValuePair<T1, KeyValuePair<T2, KeyValuePair<T3, T4>>>(TupleReadOnly<T1, T2, T3, T4> tupl)
        {
            return new KeyValuePair<T1, KeyValuePair<T2, KeyValuePair<T3, T4>>>
                (tupl.Item1, new KeyValuePair<T2, KeyValuePair<T3, T4>>(tupl.Item2, new KeyValuePair<T3, T4>(tupl.Item3, tupl.Item4)));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
