using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int GetDistance(int xFrom, int xTo, int zFrom, int zTo)
    {
        return (Mathf.Abs(xTo - xFrom) + Mathf.Abs(zTo - zFrom));
    }
}
