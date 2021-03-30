using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public const float SPEED = 4;
    private void Start()
    {
        Camera camera = new Camera();
        camera.Init();
        Player player = new Player();
        player.Init();



        Dungeon dungeon = new Dungeon();
        dungeon.NewDungeon();
    }
}
