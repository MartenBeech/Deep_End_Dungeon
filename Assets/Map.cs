using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private static int xOffset = 1700;
    private static int yOffset = 700;
    private static int maxSize;
    private static float sizeFactor = 1;
    public void AddMap(int x, int y)
    {
        AnimaMap animaMap = new AnimaMap();
        animaMap.SummonMapTile(xOffset + (x * sizeFactor), yOffset + (y * sizeFactor), x, y, sizeFactor);
    }

    public void SetMapSize(int x, int z)
    {
        maxSize = (z > x ? z : x);
        sizeFactor = 200 / maxSize;
    }
}
