using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaTerrain : MonoBehaviour
{
    public static GameObject prefab;
    public static GameObject parent;

    public void SummonTerrain()
    {
        for (int x = 0; x < Dungeon.xTiles; x++)
        {
            for (int z = 0; z < Dungeon.zTiles; z++)
            {
                switch(Terrain.types[x, z])
                {
                    case Terrain.Type.Wall:
                        SummonWall(x, z);
                        break;
                    case Terrain.Type.Path:
                        SummonPath(x, z);
                        break;
                    case Terrain.Type.Start:
                        SummonStart(x, z);
                        break;
                    case Terrain.Type.End:
                        SummonEnd(x, z);
                        break;
                }
            }
        }
    }

    public void SummonWall(int x, int z)
    {
        prefab = Resources.Load<GameObject>("Assets/Wall");
        parent = GameObject.Find("Terrain/Walls");

        GameObject newTile = Instantiate(prefab, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0), parent.transform);
        Define define = new Define();
        newTile.name = define.GetTileName("Wall", x, z);
    }

    public void SummonPath(int x, int z)
    {
        prefab = Resources.Load<GameObject>("Assets/Path");
        parent = GameObject.Find("Terrain/Paths");

        GameObject newTile = Instantiate(prefab, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0), parent.transform);
        Define define = new Define();
        newTile.name = define.GetTileName("Path", x, z);
    }

    public void SummonStart(int x, int z)
    {
        prefab = Resources.Load<GameObject>("Assets/Start");
        parent = GameObject.Find("Terrain/Stairs");

        GameObject newTile = Instantiate(prefab, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0), parent.transform);
        Define define = new Define();
        newTile.name = define.GetTileName("Start", x, z);
    }

    public void SummonEnd(int x, int z)
    {
        prefab = Resources.Load<GameObject>("Assets/End");
        parent = GameObject.Find("Terrain/Stairs");

        GameObject newTile = Instantiate(prefab, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0), parent.transform);
        Define define = new Define();
        newTile.name = define.GetTileName("End", x, z);
    }
}
