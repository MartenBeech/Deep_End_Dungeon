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

        switch(title)
        {
            case Enemy.Title.Null:
                enemy.attack = 5;
                enemy.health = 25;
                enemy.range = 1;
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
