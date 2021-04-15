using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon : MonoBehaviour
{
    public const int MAXSIZE = 10;
    public static int xTiles = MAXSIZE;
    public static int zTiles = MAXSIZE;
    private List<int> xRoomCenters = new List<int>();
    private List<int> zRoomCenters = new List<int>();

    public void NewDungeon()
    {
        xTiles = MAXSIZE;
        zTiles = MAXSIZE;
        GenerateTiles(xTiles, zTiles);
        AddRooms((xTiles / 4) + (zTiles / 4), 2, 4);
        AddRoomPaths();
        AddStairs();
        AnimaTerrain animaTerrain = new AnimaTerrain();
        animaTerrain.SummonTerrain();
        Enemy enemy = new Enemy();
        enemy.AddNormalEnemies(1);
    }

    public void GenerateTiles(int xSize, int zSize)
    {
        Terrain terrain = new Terrain();
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                terrain.AddWall(x, z);
            }
        }
    }

    public void AddRooms(int rooms, int minSize, int maxSize)
    {
        xRoomCenters.Clear();
        zRoomCenters.Clear();

        Terrain terrain = new Terrain();
        Rng rng = new Rng();
        for (int i = 0; i < rooms; i++)
        {
            int xFrom = rng.Range(1, xTiles - 1);
            int xTo = xFrom + (rng.Range(0, 2) == 0 ? rng.Range(-maxSize + 1, -minSize) : rng.Range(minSize - 1, maxSize));
            int zFrom = rng.Range(1, zTiles - 1);
            int zTo = zFrom + (rng.Range(0, 2) == 0 ? rng.Range(-maxSize + 1, -minSize) : rng.Range(minSize - 1, maxSize));

            if (xTo < xFrom)
            {
                int xTemp = xTo;
                xTo = xFrom;
                xFrom = xTemp;
            }
            if (zTo < zFrom)
            {
                int zTemp = zTo;
                zTo = zFrom;
                zFrom = zTemp;
            }

            xFrom = (xFrom < 1 ? 1 : xFrom);
            xTo = (xTo > xTiles - 2 ? xTiles - 2 : xTo);
            zFrom = (zFrom < 1 ? 1 : zFrom);
            zTo = (zTo > zTiles - 2 ? zTiles - 2 : zTo);

            terrain.AddRoom(xFrom, xTo, zFrom, zTo);
            int xCenter = xTo - ((xTo - xFrom) / 2);
            int zCenter = zTo - ((zTo - zFrom) / 2);
            xCenter = (xCenter < 1 ? 1 : xCenter);
            xCenter = (xCenter > xTiles - 2 ? xTiles - 2 : xCenter);
            zCenter = (zCenter < 1 ? 1 : zCenter);
            zCenter = (zCenter > zTiles - 2 ? zTiles - 2 : zCenter);
            xRoomCenters.Add(xCenter);
            zRoomCenters.Add(zCenter);
        }
    }

    public void AddRoomPaths()
    {
        for (int i = 0; i < xRoomCenters.Count - 1; i++)
        {
            AddPath(xRoomCenters[i], xRoomCenters[i + 1], zRoomCenters[i], zRoomCenters[i + 1]);
        }
        AddPath(xRoomCenters[xRoomCenters.Count - 1], xRoomCenters[0], zRoomCenters[zRoomCenters.Count - 1], zRoomCenters[0]);
    }

    public void AddPath(int xFrom, int xTo, int zFrom, int zTo)
    {
        Terrain terrain = new Terrain();
        Rng rng = new Rng();
        List<char> directions = new List<char>();

        int _xFrom = xFrom;
        int _xTo = xTo;
        int _zFrom = zFrom;
        int _zTo = zTo;

        while (_xFrom != _xTo || _zFrom != _zTo)
        {
            if (_xFrom < _xTo && _zFrom < _zTo)
                if (rng.Range(0, 2) == 0)
                {
                    directions.Add('E');
                    _xFrom++;
                }
                else
                {
                    directions.Add('N');
                    _zFrom++;
                }

            else if (_xFrom < _xTo && _zFrom > _zTo)
                if (rng.Range(0, 2) == 0)
                {
                    directions.Add('E');
                    _xFrom++;
                }
                else
                {
                    directions.Add('S');
                    _zFrom--;
                }

            else if (_xFrom > _xTo && _zFrom < _zTo)
                if (rng.Range(0, 2) == 0)
                {
                    directions.Add('W');
                    _xFrom--;
                }
                else
                {
                    directions.Add('N');
                    _zFrom++;
                }

            else if (_xFrom > _xTo && _zFrom > _zTo)
                if (rng.Range(0, 2) == 0)
                {
                    directions.Add('W');
                    _xFrom--;
                }
                else
                {
                    directions.Add('S');
                    _zFrom--;
                }

            else if (_xFrom < _xTo)
            {
                directions.Add('E');
                    _xFrom++;
            }
            else if (_xFrom > _xTo)
            {
                directions.Add('W');
                    _xFrom--;
            }
            else if (_zFrom < _zTo)
            {
                directions.Add('N');
                    _zFrom++;
            }
            else if (_zFrom > _zTo)
            {
                directions.Add('S');
                    _zFrom--;
            }
        }

        _xFrom = xFrom;
        _xTo = xTo;
        _zFrom = zFrom;
        _zTo = zTo;

        while (directions.Count > 0)
        {
            int direction = rng.Range(0, directions.Count);
            if (directions[direction] == 'N')
                terrain.AddPath(_xFrom, ++_zFrom);
            else if (directions[direction] == 'S')
                terrain.AddPath(_xFrom, --_zFrom);
            else if (directions[direction] == 'E')
                terrain.AddPath(++_xFrom, _zFrom);
            else if (directions[direction] == 'W')
                terrain.AddPath(--_xFrom, _zFrom);

            directions.RemoveAt(direction);
        }
    }

    public void AddStairs()
    {
        Rng rng = new Rng();
        Terrain terrain = new Terrain();
        Tile tile = new Tile();

        int xStart = 0, zStart = 0, xEnd = 0, zEnd = 0;
        do
        {
            xStart = rng.Range(1, xTiles - 1);
            zStart = rng.Range(1, zTiles - 1);
        } while (Terrain.types[xStart, zStart] != Terrain.Type.Path);
        terrain.AddStart(xStart, zStart);

        for (int i = 0; i >= 0; i++)
        {
            xEnd = rng.Range(1, xTiles - 1);
            zEnd = rng.Range(1, zTiles - 1);
            if (i < 100)
            {
                if (Terrain.types[xEnd, zEnd] == Terrain.Type.Path && tile.GetDistance(xStart, xEnd, zStart, zEnd) > ((xTiles + zTiles) / 4))
                    break;
            }
            else
            {
                if (Terrain.types[xEnd, zEnd] == Terrain.Type.Path)
                    break;
            }
        }
        terrain.AddEnd(xEnd, zEnd);
    }
}
