using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAction : MonoBehaviour
{
    public static float counter = 0f;
    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;

            if (counter <= 0)
            {
                Turn turn = new Turn();
                turn.EndTurn();
            }
        }
    }
    public void TakeAction()
    {
        Tile tile = new Tile();
        List<int> enemyPos = tile.GetNearbyEnemies(Player.scoutRange);

        if (enemyPos.Count > 0)
        {
            for (int i = 0; i < enemyPos.Count; i += 2)
            {
                int x = enemyPos[i];
                int z = enemyPos[i + 1];

                if (Obstacle.types[x, z] == Obstacle.Type.Enemy)
                {
                    if (Enemy.enemies[x, z].charging)
                    {
                        EnemyAttack enemyAttack = new EnemyAttack();
                        enemyAttack.Attack(x, z);
                    }
                    else
                    {
                        EnemyMove enemyMove = new EnemyMove();
                        enemyMove.MoveTowardsPlayer(x, z);
                    }
                }
                else
                {

                }
            }
            counter = 1.1f / Game.SPEED;
        }
        else
            counter = 0.04f / Game.SPEED;
    }
}
