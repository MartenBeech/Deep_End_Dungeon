using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static GameObject player;

    public void Init()
    {
        player = GameObject.Find("Player");
    }

    public void MovePlayer(int x, int z)
    {
        player.transform.position = new Vector3(x, 0.5f, z);
    }

    public void RotatePlayer(int degrees)
    {
        player.transform.rotation = new Quaternion(degrees, 0, 0, 0);
    }
}
