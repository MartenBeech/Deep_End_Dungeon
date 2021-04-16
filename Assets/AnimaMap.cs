using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaMap : MonoBehaviour
{
    public static GameObject prefab;
    public static GameObject parent;

    public void SummonMapTile(float x, float y, int xTile, int yTile, float sizeFactor)
    {
        prefab = Resources.Load<GameObject>("Assets/Map");
        parent = GameObject.Find("MapTile");

        GameObject newTile = Instantiate(prefab, new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0), parent.transform);
        Define define = new Define();
        newTile.name = define.GetTileName("MapTile", xTile, yTile);
        newTile.transform.localScale = new Vector3(sizeFactor * 0.95f, sizeFactor * 0.95f, 0);
    }
}
