using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class StringExtension
{
    public static string BigintToString(this BigInteger bigInt)
    {
        int placeN = 3;
        BigInteger value = bigInt;
        List<int> numList = new();
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numList.Add((int)(value % p));
            value /= p;
        } while (value >= 1);
        
        int num = numList.Count < 2 ? numList[0] : numList[numList.Count - 1] * p + numList[numList.Count - 2];
        float f = (num / (float)p);
        return numList.Count < 2 ? num.ToString() : f.ToString("N2") + GetUnitText(numList.Count - 1);
    }
    
    private static string GetUnitText(int index)
    {
        int idx = index - 1;
        if (idx < 0) return "";
        int repeatCount = (idx / 26) + 1;
        string retStr = "";
        for (int i = 0; i < repeatCount; i++)
        {
            retStr += (char)(65 + idx % 26);
        }
        return retStr;
    }
}
    