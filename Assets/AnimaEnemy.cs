using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaEnemy : MonoBehaviour
{
    public static GameObject prefab;
    public static GameObject parent;

    public void SummonEnemy(int x, int z)
    {
        prefab = Resources.Load<GameObject>("Assets/Enemy");
        parent = GameObject.Find("Enemies");

        GameObject newTile = Instantiate(prefab, new Vector3(x, 0, z), new Quaternion(0, 0, 0, 0), parent.transform);
        Define define = new Define();
        newTile.name = define.GetTileName("Enemy", x, z);
    }

    public void MoveEnemy(int xFrom, int xTo, int zFrom, int zTo)
    {

    }
}
