using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    public enum Unit
    {
        Player, Enemy
    }
    public static Unit turn = Unit.Player;

    public void EndTurn()
    {
        if (turn == Unit.Player)
            NewTurnEnemy();
        else
            NewTurnPlayer();


    }

    public void NewTurnPlayer()
    {
        turn = Unit.Player;

        Player.canMove = true;
    }

    public void NewTurnEnemy()
    {
        turn = Unit.Enemy;

        EnemyAction enemyAction = new EnemyAction();
        enemyAction.TakeAction();
    }
}
