using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private void Start()
    {
        Dungeon dungeon = new Dungeon();
        dungeon.NewDungeon();
    }
}
