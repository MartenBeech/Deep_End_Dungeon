using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum Title
    {
        Null,
    }
    public Title title;

    public int attack = 0;
    public int health = 0;
    public int range = 0;
        

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
                    if (Terrain.types[xPos, zPos] == Terrain.Type.Path && tile.GetDistance(Player.xPos, xPos, Player.zPos, zPos) > ((Dungeon.xTiles + Dungeon.zTiles) / 100))
                        break;
                }
                else
                {
                    if (Terrain.types[xPos, zPos] == Terrain.Type.Path)
                        break;
                }
            }

            SummonEnemy(Title.Null, xPos, zPos);
        }
    }

    public void SummonEnemy(Title title, int x, int z)
    {
        EnemyStats enemyStats = new EnemyStats();
        Enemy enemy = enemyStats.GetStats(title);

        AnimaEnemy animaEnemy = new AnimaEnemy();
        animaEnemy.SummonEnemy(x, z);
    }
}
