using System;

public struct ToStringWrapper
{
    private readonly Func<string> m_toString;

    public ToStringWrapper(Func<string> toString)
    {
        m_toString = toString;
    }

    public override string ToString()
    {
        return m_toString();
    }
}