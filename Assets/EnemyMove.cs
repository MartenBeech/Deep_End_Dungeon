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
            if (z < Player.zPos && !Terrain.blocked[x, z + 1] && !Object.occupied[x, z + 1])
                moveDir.Add('N');
            if (z > Player.zPos && !Terrain.blocked[x, z - 1] && !Object.occupied[x, z - 1])
                moveDir.Add('S');
            if (x < Player.xPos && !Terrain.blocked[x + 1, z] && !Object.occupied[x + 1, z])
                moveDir.Add('E');
            if (x > Player.xPos && !Terrain.blocked[x - 1, z] && !Object.occupied[x - 1, z])
                moveDir.Add('W');

            AnimaEnemy animaEnemy = new AnimaEnemy();
            GameObject prefab = GameObject.Find(define.GetTileName("Enemy", x, z));
            if (moveDir.Count > 0 && Enemy.enemies[x, z].cantMove <= 0)
            {
                Rng rng = new Rng();
                char dir = moveDir[rng.Range(0, moveDir.Count)];
                

                if (dir == 'N')
                    animaEnemy.MoveEnemy(prefab, x, x, z, z + 1);
                else if (dir == 'S')
                    animaEnemy.MoveEnemy(prefab, x, x, z, z - 1);
                else if (dir == 'E')
                    animaEnemy.MoveEnemy(prefab, x, x + 1, z, z);
                else if (dir == 'W')
                    animaEnemy.MoveEnemy(prefab, x, x - 1, z, z);
            }

            else
            {
                if (Enemy.enemies[x, z].cantMove > 0)
                    Enemy.enemies[x, z].cantMove--;
                animaEnemy.MoveEnemy(prefab, x, x, z, z);
            }
        }
    }
}
