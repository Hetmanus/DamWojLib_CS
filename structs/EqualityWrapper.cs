using System;

public struct EqualityWrapper : IEquatable<object>
{
    private readonly Func<object, bool> m_equalityFunc;
    private readonly Func<int> m_hashCode;

    public EqualityWrapper(Func<object, bool> equalityFunc, Func<int> hashCode)
    {
        m_equalityFunc = equalityFunc;
        m_hashCode = hashCode;
    }

    public override bool Equals(object obj)
    {
        return m_equalityFunc(obj);
    }
    public override int GetHashCode()
    {
        return m_hashCode();
    }
}