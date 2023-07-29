using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public static class Convertor
{
    public static long[] Replacement(string str)
    {
        return str
            .Replace("[", string.Empty)
            .Replace("]", string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray()
    }
}
