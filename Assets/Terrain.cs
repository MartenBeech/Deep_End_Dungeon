using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terrain : MonoBehaviour
{
    public enum Type
    {
        Path, Wall, Start, End,
        Null,
    }
    public static Type[,] types = new Type[Dungeon.MAXSIZE, Dungeon.MAXSIZE];

    public void AddWall(int x, int z)
    {
        types[x, z] = Type.Wall;
    }

    public void AddPath(int x, int z)
    {
        types[x, z] = Type.Path;
    }

    public void AddRoom(int xFrom, int xTo, int zFrom, int zTo)
    {
        xFrom = (xFrom < 0 ? 0 : xFrom);
        zFrom = (zFrom < 0 ? 0 : zFrom);
        xTo = (xTo > Dungeon.xTiles ? Dungeon.xTiles : xTo);
        zTo = (zTo > Dungeon.zTiles ? Dungeon.zTiles : zTo);

        for (int x = xFrom; x <= xTo; x++)
        {
            for (int z = zFrom; z <= zTo; z++)
            {
                AddPath(x, z);
            }
        }
    }

    public void AddStart(int x, int z)
    {
        types[x, z] = Type.Start;
    }

    public void AddEnd(int x, int z)
    {
        types[x, z] = Type.End;
    }
}
