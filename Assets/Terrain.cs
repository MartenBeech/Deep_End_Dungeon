using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terrain : MonoBehaviour
{
    public enum Type
    {
        Null,
        Path, Wall, Start, End,
        
    }
    public static Type[,] types = new Type[Dungeon.MAXSIZE, Dungeon.MAXSIZE];
    public static bool[,] blocked = new bool[Dungeon.MAXSIZE, Dungeon.MAXSIZE];

    public void AddWall(int x, int z)
    {
        types[x, z] = Type.Wall;
        blocked[x, z] = true;
    }

    public void AddPath(int x, int z)
    {
        types[x, z] = Type.Path;
        blocked[x, z] = false;
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
        blocked[x, z] = false;
        Player player = new Player();
        player.Move(x, x, z, z, false);
    }

    public void AddEnd(int x, int z)
    {
        types[x, z] = Type.End;
        blocked[x, z] = false;
    }
}
