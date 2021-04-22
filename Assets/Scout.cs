using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scout : MonoBehaviour
{
    public void ScoutMapTile(int x, int z)
    {
        Define define = new Define();
        if (Object.types[x, z] == Object.Type.Player)
            GameObject.Find(define.GetTileName("MapTile", x, z)).GetComponent<Image>().color = Color.HSVToRGB(120 / 360f, 1, 1);
        else if (Object.types[x, z] == Object.Type.Enemy)
            GameObject.Find(define.GetTileName("MapTile", x, z)).GetComponent<Image>().color = Color.HSVToRGB(0 / 360f, 1, 1);
        else
        {
            if (Terrain.types[x, z] == Terrain.Type.Wall)
                GameObject.Find(define.GetTileName("MapTile", x, z)).GetComponent<Image>().color = Color.HSVToRGB(0 / 360f, 0, 0.5f);
            else if (Terrain.types[x, z] == Terrain.Type.Start)
                GameObject.Find(define.GetTileName("MapTile", x, z)).GetComponent<Image>().color = Color.HSVToRGB(120 / 360f, 0.7f, 0.7f);
            else if (Terrain.types[x, z] == Terrain.Type.End)
                GameObject.Find(define.GetTileName("MapTile", x, z)).GetComponent<Image>().color = Color.HSVToRGB(0 / 360f, 0.7f, 0.7f);
            else
                GameObject.Find(define.GetTileName("MapTile", x, z)).GetComponent<Image>().color = Color.HSVToRGB(0 / 360f, 0, 1);
        }
    }

    public void ScoutPlayer()
    {
        for (int z = 0; z < Dungeon.zTiles; z++)
        {
            for (int x = 0; x < Dungeon.xTiles; x++)
            {
                ScoutMapTile(x, z);
            }
        }
    }
}
