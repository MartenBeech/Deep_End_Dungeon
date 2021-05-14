using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy[,] enemies = new Enemy[Dungeon.MAXSIZE, Dungeon.MAXSIZE];
    public enum Title
    {
        Null,
    }
    public Title title;

    public int[] attack = new int[] { 0, 0 };
    public int[] health = new int[] { 0, 0 };
    public int range = 0;
    public int[] cd = new int[] { 0, 0 };
    public int poisoned = 0;
    public int cantMove = 0;
    public int armor = 0;
    public int resistance = 0;

    public bool charging = false;
        

    public void AddNormalEnemies(int amount)
    {
        Rng rng = new Rng();
        Tile tile = new Tile();
        int xPos = 0, zPos = 0;

        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j >= 0; j++)
            {
                xPos = rng.Range(1, Dungeon.xTiles - 1);
                zPos = rng.Range(1, Dungeon.zTiles - 1);
                if (j < 100)
                {
                    if (Terrain.types[xPos, zPos] == Terrain.Type.Path && Obstacle.types[xPos, zPos] == Obstacle.Type.Null)
                        if (tile.GetDistance(Player.xPos, xPos, Player.zPos, zPos) > ((Dungeon.xTiles + Dungeon.zTiles) / 10))
                            break;
                }
                else
                {
                    if (Terrain.types[xPos, zPos] == Terrain.Type.Path && Obstacle.types[xPos, zPos] == Obstacle.Type.Null)
                        break;
                }
            }

            AddEnemy(Title.Null, xPos, zPos);
        }
    }

    public void AddEnemy(Title title, int x, int z)
    {
        EnemyStats enemyStats = new EnemyStats();
        enemies[x, z] = enemyStats.GetStats(title);
        Obstacle.types[x, z] = Obstacle.Type.Enemy;

        AnimaEnemy animaEnemy = new AnimaEnemy();
        animaEnemy.SummonEnemy(x, z);
    }

    public void Move(int xFrom, int xTo, int zFrom, int zTo)
    {
        Define define = new Define();
        GameObject enemy = GameObject.Find(define.GetTileName("Enemy", xFrom, zFrom));
        enemy.transform.position = new Vector3(xTo, Player.yPos, zTo);
        enemy.name = define.GetTileName("Enemy", xTo, zTo);

        if (xFrom != xTo || zFrom != zTo)
        {
            enemies[xTo, zTo] = new Enemy();

            Enemy posTo = enemies[xTo, zTo];
            Enemy posFrom = enemies[xFrom, zFrom];
            posTo.attack = new int[] { posFrom.attack[0], posFrom.attack[1] };
            posTo.health = new int[] { posFrom.health[0], posFrom.health[1] };
            posTo.range = posFrom.range;
            posTo.cd = new int[] { posFrom.cd[0], posFrom.cd[1] };
            posTo.poisoned = posFrom.poisoned;
            posTo.cantMove = posFrom.cantMove;
            posTo.armor = posFrom.armor;
            posTo.resistance = posFrom.resistance;

            posTo.charging = posFrom.charging;

            enemies[xFrom, zFrom] = null;
        }

        if (enemies[xTo, zTo].cd[0] > 0)
            enemies[xTo, zTo].cd[0]--;

        if (enemies[xTo, zTo].cd[0] <= 0)
        {
            Tile tile = new Tile();
            if (tile.GetDistance(xTo, Player.xPos, zTo, Player.zPos) <= enemies[xTo, zTo].range)
            {
                EnemyAttack enemyAttack = new EnemyAttack();
                enemyAttack.Charge(xTo, zTo);
            }
        }
    }

    public void Kill(int x, int z)
    {
        Define define = new Define();
        Destroy(GameObject.Find(define.GetTileName("Enemy", x, z)));
        Destroy(GameObject.Find(define.GetTileName("EnemyHealthBar", x, z)));
        enemies[x, z] = null;
        Obstacle obstacle = new Obstacle();
        obstacle.Kill(x, z);

        Scout scout = new Scout();
        scout.ScoutPlayer();
    }
}
