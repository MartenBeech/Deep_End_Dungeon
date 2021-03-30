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
}
