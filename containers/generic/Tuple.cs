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
    public class Tuple<T1, T2> : TupleProtected<T1, T2>
    {
        public new T1 Item1 { get { return base.Item1; } set { base.Item1 = value; } }
        public new T2 Item2 { get { return base.Item2; } set { base.Item2 = value; } }
        public Tuple() { }
        public Tuple(T1 value1, T2 value2) : base(value1, value2) { }
        public Tuple(KeyValuePair<T1, T2> data) : base(data) { }
    }
    [Serializable]
    public class Tuple<T1, T2, T3> : TupleProtected<T1, T2, T3>
    {
        public new T1 Item1 { get { return base.Item1; } set { base.Item1 = value; } }
        public new T2 Item2 { get { return base.Item2; } set { base.Item2 = value; } }
        public new T3 Item3 { get { return base.Item3; } set { base.Item3 = value; } }
        public Tuple() { }
        public Tuple(T1 value1, T2 value2, T3 value3) : base(value1, value2, value3) { }
        public Tuple(KeyValuePair<T1, KeyValuePair<T2, T3>> data) : base(data) { }
    }
    [Serializable]
    public class Tuple<T1, T2, T3, T4> : TupleProtected<T1, T2, T3, T4>
    {
        public new T1 Item1 { get { return base.Item1; } set { base.Item1 = value; } }
        public new T2 Item2 { get { return base.Item2; } set { base.Item2 = value; } }
        public new T3 Item3 { get { return base.Item3; } set { base.Item3 = value; } }
        public new T4 Item4 { get { return base.Item4; } set { base.Item4 = value; } }
        public Tuple() { }
        public Tuple(T1 value1, T2 value2, T3 value3, T4 value4) : base(value1, value2, value3, value4) { }
        public Tuple(KeyValuePair<T1, KeyValuePair<T2, KeyValuePair<T3, T4>>> data) : base(data) { }
    }

    [Serializable]
    public abstract class TupleProtected<T1, T2>
    {
        protected T1 Item1;
        protected T2 Item2;
        public TupleProtected() { }
        public TupleProtected(T1 value1, T2 value2)
        {
            Item1 = value1;
            Item2 = value2;
        }
        public TupleProtected(KeyValuePair<T1, T2> data)
        {
            Item1 = data.Key;
            Item2 = data.Value;
        }

        public static bool operator ==(TupleProtected<T1, T2> x, TupleStruct<T1, T2> y)
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
        public static bool operator !=(TupleProtected<T1, T2> x, TupleStruct<T1, T2> y)
        {
            return !(x == y);
        }
        public static bool operator ==(TupleProtected<T1, T2> x, TupleReadOnly<T1, T2> y)
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
        public static bool operator !=(TupleProtected<T1, T2> x, TupleReadOnly<T1, T2> y)
        {
            return !(x == y);
        }

        public static implicit operator TupleStruct<T1, T2>(TupleProtected<T1, T2> tupl)
        {
            return new TupleStruct<T1, T2>(tupl.Item1, tupl.Item2);
        }
        public static implicit operator TupleReadOnly<T1, T2>(TupleProtected<T1, T2> tupl)
        {
            return new TupleReadOnly<T1, T2>(tupl.Item1, tupl.Item2);
        }
        public static implicit operator KeyValuePair<T1, T2>(TupleProtected<T1, T2> tupl)
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
    public abstract class TupleProtected<T1, T2, T3> : TupleProtected<T1, T2>
    {
        protected T3 Item3;
        public TupleProtected() { }
        public TupleProtected(T1 value1, T2 value2, T3 value3)
        {
            Item1 = value1;
            Item2 = value2;
            Item3 = value3;
        }
        public TupleProtected(KeyValuePair<T1, KeyValuePair<T2, T3>> data)
        {
            Item1 = data.Key;
            Item2 = data.Value.Key;
            Item3 = data.Value.Value;
        }

        public static bool operator ==(TupleProtected<T1, T2, T3> x, TupleStruct<T1, T2, T3> y)
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
        public static bool operator !=(TupleProtected<T1, T2, T3> x, TupleStruct<T1, T2, T3> y)
        {
            return !(x == y);
        }
        public static bool operator ==(TupleProtected<T1, T2, T3> x, TupleReadOnly<T1, T2, T3> y)
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
        public static bool operator !=(TupleProtected<T1, T2, T3> x, TupleReadOnly<T1, T2, T3> y)
        {
            return !(x == y);
        }

        public static implicit operator TupleStruct<T1, T2, T3>(TupleProtected<T1, T2, T3> tupl)
        {
            return new TupleStruct<T1, T2, T3>(tupl.Item1, tupl.Item2, tupl.Item3);
        }
        public static implicit operator TupleReadOnly<T1, T2, T3>(TupleProtected<T1, T2, T3> tupl)
        {
            return new TupleReadOnly<T1, T2, T3>(tupl.Item1, tupl.Item2, tupl.Item3);
        }
        public static implicit operator KeyValuePair<T1, KeyValuePair<T2, T3>>(TupleProtected<T1, T2, T3> tupl)
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
    public abstract class TupleProtected<T1, T2, T3, T4> : TupleProtected<T1, T2, T3>
    {
        protected T4 Item4;
        public TupleProtected() { }
        public TupleProtected(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            Item1 = value1;
            Item2 = value2;
            Item3 = value3;
            Item4 = value4;
        }
        public TupleProtected(KeyValuePair<T1, KeyValuePair<T2, KeyValuePair<T3, T4>>> data)
        {
            Item1 = data.Key;
            Item2 = data.Value.Key;
            Item3 = data.Value.Value.Key;
            Item4 = data.Value.Value.Value;
        }

        public static bool operator ==(TupleProtected<T1, T2, T3, T4> x, TupleStruct<T1, T2, T3, T4> y)
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
        public static bool operator !=(TupleProtected<T1, T2, T3, T4> x, TupleStruct<T1, T2, T3, T4> y)
        {
            return !(x == y);
        }
        public static bool operator ==(TupleProtected<T1, T2, T3, T4> x, TupleReadOnly<T1, T2, T3, T4> y)
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
        public static bool operator !=(TupleProtected<T1, T2, T3, T4> x, TupleReadOnly<T1, T2, T3, T4> y)
        {
            return !(x == y);
        }

        public static implicit operator TupleStruct<T1, T2, T3, T4>(TupleProtected<T1, T2, T3, T4> tupl)
        {
            return new TupleStruct<T1, T2, T3, T4>(tupl.Item1, tupl.Item2, tupl.Item3, tupl.Item4);
        }
        public static implicit operator TupleReadOnly<T1, T2, T3, T4>(TupleProtected<T1, T2, T3, T4> tupl)
        {
            return new TupleReadOnly<T1, T2, T3, T4>(tupl.Item1, tupl.Item2, tupl.Item3, tupl.Item4);
        }
        public static implicit operator KeyValuePair<T1, KeyValuePair<T2, KeyValuePair<T3, T4>>>(TupleProtected<T1, T2, T3, T4> tupl)
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
