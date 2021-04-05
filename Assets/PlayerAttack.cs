using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public int abilitySelected = -1;
    public enum Ability
    {
        Sword, Crossbow,
        PoisonDart, Incinerate,
        FrostNova, ShieldBlock,
        DeathsBlade
    }

    public void UseAbility(Ability ability, int x, int z)
    {
        switch(ability)
        {
            case Ability.Sword:
                DealDamage(x, z, 10, 20);
                break;

            case Ability.Crossbow:
                DealDamage(x, z, 5, 10);
                break;

            case Ability.PoisonDart:
                Enemy.enemies[x, z].poisoned += 3;
                break;

            case Ability.Incinerate:
                Enemy.enemies[x, z].armor -= 5;
                Enemy.enemies[x, z].resistance -= 5;
                break;

            case Ability.FrostNova:
                Tile tile = new Tile();
                List<int> enemyPos = tile.GetNearbyEnemies(Player.scoutRange);

                for (int i = 0; i < enemyPos.Count; i += 2)
                {
                    int xPos = enemyPos[i];
                    int zPos = enemyPos[i + 1];

                    Enemy.enemies[xPos, zPos].cantMove = 3;
                }
                break;

            case Ability.ShieldBlock:
                Player.shieldBlocked = true;
                break;

            case Ability.DeathsBlade:

                break;
        }
    }

    public void SelectAbility(int i)
    {
        abilitySelected = i;
    }

    public void DeselectAbility()
    {
        abilitySelected = -1;
    }

    public void DealDamage(int x, int z, int dmgMin, int dmgMax)
    {
        Rng rng = new Rng();
        int damage = rng.Range(dmgMin, dmgMax + 1);
    }
}
