using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object : MonoBehaviour
{
    public enum Type
    {
        Null,
        Player, Enemy,
    }

    public static Type[,] types = new Type[Dungeon.MAXSIZE, Dungeon.MAXSIZE];
    public static bool[,] occupied = new bool[Dungeon.MAXSIZE, Dungeon.MAXSIZE];

    public void EnemyClicked(int x, int z)
    {

    }
}