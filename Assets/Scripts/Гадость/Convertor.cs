using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public static class Convertor
{
    public static long[] Replacement(string str)
    {
        str = str.Replace("[", ""); // удаляем символы "["
        str = str.Replace("]", ""); // удаляем символы "]"
        string[] strArray = str.Split(','); // разделяем строку по запятым
        return Array.ConvertAll(strArray, long.Parse); // преобразуем каждый элемент строки в число
    }
}
