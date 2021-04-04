using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Define : MonoBehaviour
{
    public string GetTileName(string type, int x, int z)
    {
        return type + " (" + x + "," + z + ")";
    }

    public int[] GetTilePos(string name)
    {
        int index1 = 0, index2 = 0, index3 = 0;
        for (int i = 0; i < name.Length; i++)
        {
            if (name[i] == '(')
                index1 = i;
            else if (name[i] == ',')
                index2 = i;
            else if (name[i] == ')')
                index3 = i;
        }
        string x = name.Substring(index1 + 1, index2 - (index1 + 1));
        string z = name.Substring(index2 + 1, index3 - (index2 + 1));
        return new int[] { int.Parse(x), int.Parse(z) };
    }
}
