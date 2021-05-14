using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
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
        if (PlayerAttack.abilitySelected > -1)
        {
            PlayerAttack playerAttack = new PlayerAttack();
            playerAttack.UseAbility(PlayerAttack.abilitySelected, x, z);
        }
    }

    public void Kill(int x, int z)
    {
        types[x, z] = Type.Null;
        occupied[x, z] = false;
    }
}
