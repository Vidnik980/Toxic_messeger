using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public static class Convertor
{
    public static long[] Replacement(string str)
    {
        str = str.Replace("[", ""); // ������� ������� "["
        str = str.Replace("]", ""); // ������� ������� "]"
        string[] strArray = str.Split(','); // ��������� ������ �� �������
        return Array.ConvertAll(strArray, long.Parse); // ����������� ������ ������� ������ � �����
    }
}
