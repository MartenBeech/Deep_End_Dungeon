using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static GameObject player;
    public static float angle = 0f;
    public const float yPos = 0.5f;
    public static int xPos = 0;
    public static int zPos = 0;
    private static bool canMove = true;

    
    public static Tile.Direction faceDirection = Tile.Direction.N;

    public void Init()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetKey("w"))
            {
                Tile tile = new Tile();
                if (tile.GetPassableInFront(xPos, zPos, faceDirection))
                {
                    AnimaPlayer animaPlayer = new AnimaPlayer();
                    if (faceDirection == Tile.Direction.N)
                        animaPlayer.MovePlayer(xPos, zPos + 1);
                    else if (faceDirection == Tile.Direction.S)
                        animaPlayer.MovePlayer(xPos, zPos - 1);
                    else if (faceDirection == Tile.Direction.E)
                        animaPlayer.MovePlayer(xPos + 1, zPos);
                    else if (faceDirection == Tile.Direction.W)
                        animaPlayer.MovePlayer(xPos - 1, zPos);
                    canMove = false;
                }
            }
            else if (Input.GetKey("s"))
            {
                Tile tile = new Tile();
                if (tile.GetPassableBehind(xPos, zPos, faceDirection))
                {
                    AnimaPlayer animaPlayer = new AnimaPlayer();
                    if (faceDirection == Tile.Direction.N)
                        animaPlayer.MovePlayer(xPos, zPos - 1);
                    else if (faceDirection == Tile.Direction.S)
                        animaPlayer.MovePlayer(xPos, zPos + 1);
                    else if (faceDirection == Tile.Direction.E)
                        animaPlayer.MovePlayer(xPos - 1, zPos);
                    else if (faceDirection == Tile.Direction.W)
                        animaPlayer.MovePlayer(xPos + 1, zPos);
                    canMove = false;
                }
            }
            else if (Input.GetKey("a"))
            {
                AnimaPlayer animaPlayer = new AnimaPlayer();
                animaPlayer.RotatePlayer(-90);
                canMove = false;
            }
            else if (Input.GetKey("d"))
            {
                AnimaPlayer animaPlayer = new AnimaPlayer();
                animaPlayer.RotatePlayer(90);
                canMove = false;
            }
        }
    }

    public void MovePlayer(int x, int z)
    {
        player.transform.position = new Vector3(x, yPos, z);
        xPos = x;
        zPos = z;
        canMove = true;
    }

    public void RotatePlayer(float degrees)
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
