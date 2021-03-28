using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dungeon : MonoBehaviour
{
    public const int MAXSIZE = 100;
    public static int xTiles = MAXSIZE;
    public static int zTiles = MAXSIZE;
    private List<int> xRoomCenters = new List<int>();
    private List<int> zRoomCenters = new List<int>();

    public void NewDungeon()
    {
        xTiles = 50;
        zTiles = 50;
        GenerateTiles(xTiles, zTiles);
        AddRooms(50, 2, 4);
        AddRoomPaths();
        AddStairs();
        AnimaTerrain animaTerrain = new AnimaTerrain();
        animaTerrain.SummonTerrain();
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
            int xFrom = rng.Range(0, xTiles);
            int xTo = xFrom + (rng.Range(0, 2) == 0 ? rng.Range(-maxSize + 1, -minSize) : rng.Range(minSize - 1, maxSize));
            int zFrom = rng.Range(0, zTiles);
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

            xFrom = (xFrom < 0 ? 0 : xFrom);
            xTo = (xTo >= xTiles ? xTiles - 1 : xTo);
            zFrom = (zFrom < 0 ? 0 : zFrom);
            zTo = (zTo >= zTiles ? zTiles - 1 : zTo);

            terrain.AddRoom(xFrom, xTo, zFrom, zTo);
            int xCenter = xTo - ((xTo - xFrom) / 2);
            int zCenter = zTo - ((zTo - zFrom) / 2);
            xCenter = (xCenter < 0 ? 0 : xCenter);
            xCenter = (xCenter >= xTiles ? xTiles - 1 : xCenter);
            zCenter = (zCenter < 0 ? 0 : zCenter);
            zCenter = (zCenter >= zTiles ? zTiles - 1 : zCenter);
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

        int xStart, zStart, xEnd, zEnd;
        do
        {
            xStart = rng.Range(0, xTiles);
            zStart = rng.Range(0, zTiles);
        } while (Terrain.types[xStart, zStart] != Terrain.Type.Path);
        terrain.AddStart(xStart, zStart);

        do
        {
            xEnd = rng.Range(0, xTiles);
            zEnd = rng.Range(0, zTiles);
        } while (Terrain.types[xEnd, zEnd] != Terrain.Type.Path || tile.GetDistance(xStart, xEnd, zStart, zEnd) < ((xTiles + zTiles) / 4));
        terrain.AddEnd(xEnd, zEnd);
    }
}
