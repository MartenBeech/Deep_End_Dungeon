using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public static int abilitySelected = -1;
    public static int[] cooldown = new int[7];
    public static int[] manaCost = new int[7] { 0, 0, 3, 3, 15, 15, 50 };
    public static int[] coolDown = new int[7] { 0, 0, 0, 0, 10, 10, 30 };
    public enum Ability
    {
        Sword, Crossbow,
        PoisonDart, Incinerate,
        FrostNova, ShieldBlock,
        DeathsBlade
    }


    public void UseAbility(int id, int x, int z)
    {
        Tile tile = new Tile();
        Turn turn = new Turn();

        if (cooldown[id] == 0)
        {
                Ability ability = (Ability)id;
                switch (ability)
                {
                    case Ability.Sword:
                        if (tile.GetDistance(Player.xPos, x, Player.zPos, z) < 2)
                        {
                            if (UseMana(manaCost[id]))
                            {
                                DealDamage(x, z, 10, 20);
                                turn.EndTurn();
                            }
                        }
                        break;

                    case Ability.Crossbow:
                        if (UseMana(manaCost[id]))
                        {
                            DealDamage(x, z, 5, 10);
                            turn.EndTurn();
                        }
                        break;

                    case Ability.PoisonDart:
                        if (UseMana(manaCost[id]))
                        {
                            Enemy.enemies[x, z].poisoned += 3;
                            turn.EndTurn();
                        }
                        break;

                    case Ability.Incinerate:
                        if (UseMana(manaCost[id]))
                        {
                            Enemy.enemies[x, z].armor -= 5;
                            Enemy.enemies[x, z].resistance -= 5;
                            turn.EndTurn();
                        }
                        break;

                    case Ability.FrostNova:
                        List<int> enemyPos = tile.GetNearbyEnemies(Player.scoutRange);

                        if (enemyPos.Count > 0)
                        {
                            if (UseMana(manaCost[id]))
                            {
                                for (int i = 0; i < enemyPos.Count; i += 2)
                                {
                                    int xPos = enemyPos[i];
                                    int zPos = enemyPos[i + 1];

                                    Enemy.enemies[xPos, zPos].cantMove = 3;
                                }
                                turn.EndTurn();
                            }
                        }
                        break;

                    case Ability.ShieldBlock:
                        if (UseMana(manaCost[id]))
                        {
                            Player.shieldBlocked = true;
                            turn.EndTurn();
                        }
                        break;

                    case Ability.DeathsBlade:

                        break;
                }
            
        }
    }

    public void SelectAbility(int i)
    {
        if (abilitySelected == i)
            DeselectAbility();
        else
        {
            abilitySelected = i;
            if (abilitySelected == 4 || abilitySelected == 5)
            {
                UseAbility(abilitySelected, Player.xPos, Player.zPos);
                DeselectAbility();
            }
        }
    }

    public void DeselectAbility()
    {
        abilitySelected = -1;
    }

    public void DealDamage(int x, int z, int dmgMin, int dmgMax)
    {
        Rng rng = new Rng();
        int damage = rng.Range(dmgMin, dmgMax + 1);
        Enemy.enemies[x, z].health[0] -= damage;
        Define define = new Define();
        GameObject enemyHealthBar = GameObject.Find(define.GetTileName("EnemyHealthBar", x, z));
        enemyHealthBar.GetComponentInChildren<Text>().text = Enemy.enemies[x, z].health[0] + "/" + Enemy.enemies[x, z].health[1];
        enemyHealthBar.GetComponent<Image>().fillAmount = (float)Enemy.enemies[x, z].health[0] / (float)Enemy.enemies[x, z].health[1];
    }

    public bool UseMana(int amount)
    {
        if (Player.mana[0] >= amount)
        {
            Player.mana[0] -= amount;
            return true;
        }
        return false;
    }
}
