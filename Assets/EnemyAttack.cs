using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public void Charge(int x, int z)
    {
        Enemy dealer = Enemy.enemies[x, z];
        dealer.charging = true;
    }

    public void Attack(int x, int z)
    {
        Enemy dealer = Enemy.enemies[x, z];
        dealer.charging = false;

        Rng rng = new Rng();
        Player.health[0] -= rng.Range(dealer.attack[0], dealer.attack[1] + 1);
        Enemy.enemies[x, z].cd[0] = Enemy.enemies[x, z].cd[1];
    }
}
