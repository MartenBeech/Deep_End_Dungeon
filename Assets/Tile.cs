using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public enum Direction
    {
        N, S, E, W, NE, NW, SE, SW
    };

    public int GetDistance(int xFrom, int xTo, int zFrom, int zTo)
    {
        return (Mathf.Abs(xTo - xFrom) + Mathf.Abs(zTo - zFrom));
    }

    public bool GetPassableInFront(int x, int z, Direction direction)
    {
        switch(direction)
        {
            case Direction.N:
                return Terrain.passable[x, z + 1];
            case Direction.S:
                return Terrain.passable[x, z - 1];
            case Direction.E:
                return Terrain.passable[x + 1, z];
            case Direction.W:
                return Terrain.passable[x - 1, z];
        }
        return false;
    }

    public bool GetPassableBehind(int x, int z, Direction direction)
    {
        switch (direction)
        {
            case Direction.N:
                return Terrain.passable[x, z - 1];
            case Direction.S:
                return Terrain.passable[x, z + 1];
            case Direction.E:
                return Terrain.passable[x - 1, z];
            case Direction.W:
                return Terrain.passable[x + 1, z];
        }
        return false;
    }

    public int[,] GetNearbyEnemies(int range)
    {
        int xMin = (Player.xPos - range < 1 ? 1 : Player.xPos - range);
        int zMin = (Player.zPos - range < 1 ? 1 : Player.zPos - range);
        int xMax = (Player.xPos + range > Dungeon.xTiles ? Dungeon.xTiles : Player.xPos + range);
        int zMax = (Player.zPos + range > Dungeon.zTiles ? Dungeon.zTiles : Player.zPos + range);

        List<int> xPos = new List<int>();
        List<int> zPos = new List<int>();
        Define define = new Define();

        for (int i = xMin; i <= xMax; i++)
        {
            for (int j = zMin; j < zMax; j++)
            {
                if (GameObject.Find(define.GetTileName("Enemy", i, j)))
                {
                    xPos.Add(i);
                    zPos.Add(j);
                }
            }
        }

        int[,] enemyPos = new int[2, zPos.Count];
        for (int i = 0; i < enemyPos.Length; i++)
        {
            enemyPos[0, i] = xPos[i];
            enemyPos[1, i] = zPos[i];
        }
        return enemyPos;
    }
}
