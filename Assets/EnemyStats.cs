using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public Enemy GetStats(Enemy.Title title)
    {
        Enemy enemy = ReturnDefaults(title);

        switch (title)
        {
            case Enemy.Title.Null:
                enemy.attack = new int[] {5, 10};
                enemy.health = new int[] { 25, 25 };
                enemy.range = 1;
                enemy.cd = new int[] { 0, 0 };
                break;
        }

        return enemy;
    }

    private Enemy ReturnDefaults(Enemy.Title title)
    {
        Enemy enemy = new Enemy();
        enemy.title = title;

        return enemy;
    }
}
