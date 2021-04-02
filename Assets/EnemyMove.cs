using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public void MoveTowardsPlayer(int x, int z)
    {
        Define define = new Define();
        if (GameObject.Find(define.GetTileName("Enemy", x, z)))
        {
            List<char> moveDir = new List<char>();
            if (z < Player.zPos && Terrain.passable[x, z + 1])
                moveDir.Add('N');
            if (z > Player.zPos && Terrain.passable[x, z - 1])
                moveDir.Add('S');
            if (x < Player.xPos && Terrain.passable[x + 1, z])
                moveDir.Add('E');
            if (x > Player.xPos && Terrain.passable[x - 1, z])
                moveDir.Add('W');

            if (moveDir.Count > 0)
            {
                Rng rng = new Rng();
                char dir = moveDir[rng.Range(0, moveDir.Count)];
                AnimaEnemy animaEnemy = new AnimaEnemy();

                if (dir == 'N')
                    animaEnemy.MoveEnemy(x, x, z, z + 1);
                else if (dir == 'S')
                    animaEnemy.MoveEnemy(x, x, z, z - 1);
                else if (dir == 'E')
                    animaEnemy.MoveEnemy(x, x + 1, z, z);
                else if (dir == 'W')
                    animaEnemy.MoveEnemy(x, x - 1, z, z);
            }
        }
    }
}
