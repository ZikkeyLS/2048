using System;
using UnityEngine;

[Serializable]
public class LocalizedValue
{
    [Multiline] public string Format;

    public string Result(object[] objects)
    {
        string result = Format;
        for (int i = 0; i < objects.Length; i++)
            result = ReplaceFirst(result, "{}", objects[i].ToString());
        return result;
    }

    private string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search);

        if (pos < 0)
            return "Can't Format";

        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }
}
