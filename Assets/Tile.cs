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

    public float GetDistance(int xFrom, int xTo, int zFrom, int zTo)
    {
        int xDis = Mathf.Abs(xTo - xFrom);
        int zDis = Mathf.Abs(zTo - zFrom);
        return Mathf.Sqrt(Mathf.Pow(xDis, 2) + Mathf.Pow(zDis, 2));
    }

    public bool GetBlockedInFront(int x, int z, Direction direction)
    {
        switch(direction)
        {
            case Direction.N:
                return Terrain.blocked[x, z + 1] | Obstacle.occupied[x, z + 1];
            case Direction.S:
                return Terrain.blocked[x, z - 1] | Obstacle.occupied[x, z - 1];
            case Direction.E:
                return Terrain.blocked[x + 1, z] | Obstacle.occupied[x + 1, z];
            case Direction.W:
                return Terrain.blocked[x - 1, z] | Obstacle.occupied[x - 1, z];
        }
        return false;
    }

    public bool GetBlockedBehind(int x, int z, Direction direction)
    {
        switch (direction)
        {
            case Direction.N:
                return Terrain.blocked[x, z - 1] | Obstacle.occupied[x, z - 1];
            case Direction.S:
                return Terrain.blocked[x, z + 1] | Obstacle.occupied[x, z + 1];
            case Direction.E:
                return Terrain.blocked[x - 1, z] | Obstacle.occupied[x - 1, z];
            case Direction.W:
                return Terrain.blocked[x + 1, z] | Obstacle.occupied[x + 1, z];
        }
        return false;
    }

    public List<int> GetNearbyEnemies(int range)
    {
        int xMin = (Player.xPos - range < 1 ? 1 : Player.xPos - range);
        int zMin = (Player.zPos - range < 1 ? 1 : Player.zPos - range);
        int xMax = (Player.xPos + range > Dungeon.xTiles - 2 ? Dungeon.xTiles - 2 : Player.xPos + range);
        int zMax = (Player.zPos + range > Dungeon.zTiles - 2 ? Dungeon.zTiles - 2 : Player.zPos + range);

        List<int> enemyPos = new List<int>();
        Define define = new Define();

        for (int dist = 1; dist <= range; dist++)
        {
            for (int i = xMin; i <= xMax; i++)
            {
                for (int j = zMin; j <= zMax; j++)
                {
                    float distance = GetDistance(i, Player.xPos, j, Player.zPos);
                    if (distance <= dist && distance > dist - 1)
                    {
                        if (Obstacle.types[i, j] == Obstacle.Type.Enemy)
                        {
                            enemyPos.Add(i);
                            enemyPos.Add(j);
                        }
                    }
                }
            }
        }
        return enemyPos;
    }
}
