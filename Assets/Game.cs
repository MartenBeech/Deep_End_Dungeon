using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public const float SPEED = 4;

    Ray ray;
    RaycastHit hit;
    private void Start()
    {
        Cam cam = new Cam();
        cam.Init();
        Player player = new Player();
        player.Init();



        Dungeon dungeon = new Dungeon();
        dungeon.NewDungeon();
    }

    private void Update()
    {
        if (Player.canMove)
        {
            if (Input.GetKey("w"))
            {
                Tile tile = new Tile();
                if (!tile.GetBlockedInFront(Player.xPos, Player.zPos, Player.faceDirection))
                {
                    AnimaPlayer animaPlayer = new AnimaPlayer();
                    if (Player.faceDirection == Tile.Direction.N)
                        animaPlayer.MovePlayer(Player.xPos, Player.zPos + 1);
                    else if (Player.faceDirection == Tile.Direction.S)
                        animaPlayer.MovePlayer(Player.xPos, Player.zPos - 1);
                    else if (Player.faceDirection == Tile.Direction.E)
                        animaPlayer.MovePlayer(Player.xPos + 1, Player.zPos);
                    else if (Player.faceDirection == Tile.Direction.W)
                        animaPlayer.MovePlayer(Player.xPos - 1, Player.zPos);
                    Player.canMove = false;
                }
            }
            else if (Input.GetKey("s"))
            {
                Tile tile = new Tile();
                if (!tile.GetBlockedBehind(Player.xPos, Player.zPos, Player.faceDirection))
                {
                    AnimaPlayer animaPlayer = new AnimaPlayer();
                    if (Player.faceDirection == Tile.Direction.N)
                        animaPlayer.MovePlayer(Player.xPos, Player.zPos - 1);
                    else if (Player.faceDirection == Tile.Direction.S)
                        animaPlayer.MovePlayer(Player.xPos, Player.zPos + 1);
                    else if (Player.faceDirection == Tile.Direction.E)
                        animaPlayer.MovePlayer(Player.xPos - 1, Player.zPos);
                    else if (Player.faceDirection == Tile.Direction.W)
                        animaPlayer.MovePlayer(Player.xPos + 1, Player.zPos);
                    Player.canMove = false;
                }
            }
            else if (Input.GetKey("a"))
            {
                AnimaPlayer animaPlayer = new AnimaPlayer();
                animaPlayer.RotatePlayer(-90);
                Player.canMove = false;
            }
            else if (Input.GetKey("d"))
            {
                AnimaPlayer animaPlayer = new AnimaPlayer();
                animaPlayer.RotatePlayer(90);
                Player.canMove = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Object obj = new Object();
                if (hit.collider.name.Contains("Enemy"))
                    obj.EnemyClicked();
            }
        }
    }

    public void EnemyClicked()
    {

    }
}
