//----------------------------------------------------------------------------
//
// Author: Damian Wojcik
// Creation: N/A
// Note: Library Constant variables
//
//----------------------------------------------------------------------------

using System.Collections.Generic;

public static class Consts
{
    public static IEnumerable<string> assemblyQulifiedFormats
    {
        get
        {
            yield return "{0}{1}, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
            yield return "{0}{1}, Assembly-CSharp-Editor, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
        }
    }
    public const char spaceSymbolPlaceholder = '_';
}