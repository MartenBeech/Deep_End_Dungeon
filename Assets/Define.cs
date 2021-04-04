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
        for (int i = 0; i < name.Length; i++)
        {
            
        }
        return new int[] { 0 };
    }
}
