using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static GameObject player;
    public static float angle = 0f;
    public const float yPos = 0.25f;
    public static int xPos = 0;
    public static int zPos = 0;
    public static bool canMove = true;
    public static int scoutRange = 5;
    public static bool shieldBlocked = false;

    public static int[] health = new int[] { 50, 50 };
    public static int[] mana = new int[] { 50, 50 };


    public static Tile.Direction faceDirection = Tile.Direction.N;

    public void Init()
    {
        player = GameObject.Find("Player");
    }

    public void Move(int xFrom, int xTo, int zFrom, int zTo, bool endTurn = true)
    {
        player.transform.position = new Vector3(xTo, yPos, zTo);
        xPos = xTo;
        zPos = zTo;

        Object.types[xFrom, zFrom] = Object.Type.Null;
        Object.occupied[xFrom, zFrom] = false;
        Object.types[xTo, zTo] = Object.Type.Player;
        Object.occupied[xTo, zTo] = true;

        if (endTurn)
        {
            Turn turn = new Turn();
            turn.EndTurn();
        }
    }

    public void Rotate(float degrees)
    {
        angle = degrees;
        if (angle <= -2 * Mathf.PI)
            angle += 2 * Mathf.PI;
        else if (angle >= 2 * Mathf.PI)
            angle -= 2 * Mathf.PI;

        player.transform.rotation = new Quaternion(player.transform.rotation.x, Mathf.Sin(degrees / 2), player.transform.rotation.z, Mathf.Cos(degrees / 2));

        if (Mathf.Round(angle * 100f) == 0)
            faceDirection = Tile.Direction.N;
        else if (Mathf.Round(angle * 100f) == Mathf.Round(Mathf.PI * 100f) || (Mathf.Round(angle * 100f) == -Mathf.Round(Mathf.PI * 100f)))
            faceDirection = Tile.Direction.S;
        else if (Mathf.Round(angle * 100f) == Mathf.Round(Mathf.PI * 100f) / 2 || (Mathf.Round(angle * 100f) == -3 * Mathf.Round(Mathf.PI * 100f) / 2))
            faceDirection = Tile.Direction.E;
        else if (Mathf.Round(angle * 100f) == -Mathf.Round(Mathf.PI * 100f) / 2 || (Mathf.Round(angle * 100f) == 3 * Mathf.Round(Mathf.PI * 100f) / 2))
            faceDirection = Tile.Direction.W;

        canMove = true;
    }
}
