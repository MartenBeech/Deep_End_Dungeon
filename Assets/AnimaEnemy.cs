using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaEnemy : MonoBehaviour
{
    public static GameObject prefab;
    public static GameObject parent;

    public int xStart;
    public int zStart;
    public int xEnd;
    public int zEnd;

    public float moveCounter = 0f;

    private void Update()
    {
        if (moveCounter > 0)
        {
            Vector3 dir = new Vector3(xEnd, 0, zEnd) - new Vector3(xStart, 0, zStart);
            float dist = Mathf.Sqrt(
                Mathf.Pow(xEnd - xStart, 2) +
                Mathf.Pow(zEnd - zStart, 2));
            this.transform.position += (dir.normalized * dist * (Time.deltaTime) * Game.SPEED);
            moveCounter -= Time.deltaTime;

            if (moveCounter <= 0)
            {
                Enemy enemy = new Enemy();
                enemy.Move(xStart, xEnd, zStart, zEnd);
            }
        }

        this.transform.rotation = new Quaternion(Player.player.transform.rotation.x, Player.player.transform.rotation.y, Player.player.transform.rotation.z, Player.player.transform.rotation.w);
    }


    public void SummonEnemy(int x, int z)
    {
        prefab = Resources.Load<GameObject>("Assets/Enemy");
        parent = GameObject.Find("Enemies");

        GameObject newTile = Instantiate(prefab, new Vector3(x, Player.yPos, z), new Quaternion(0, 0, 0, 0), parent.transform);
        Define define = new Define();
        newTile.name = define.GetTileName("Enemy", x, z);

        AnimaEnemyHealthBar animaEnemyHealthBar = new AnimaEnemyHealthBar();
        animaEnemyHealthBar.SummonEnemyHealthBar(x, z);
    }

    public void MoveEnemy(GameObject gameObject, int xFrom, int xTo, int zFrom, int zTo)
    {
        gameObject.GetComponentInChildren<AnimaEnemy>().xStart = xFrom;
        gameObject.GetComponentInChildren<AnimaEnemy>().xEnd = xTo;
        gameObject.GetComponentInChildren<AnimaEnemy>().zStart = zFrom;
        gameObject.GetComponentInChildren<AnimaEnemy>().zEnd = zTo;
        gameObject.GetComponentInChildren<AnimaEnemy>().moveCounter += 1 / Game.SPEED;

        Object.types[xFrom, zFrom] = Object.Type.Null;
        Object.occupied[xFrom, zFrom] = false;
        Object.types[xTo, zTo] = Object.Type.Enemy;
        Object.occupied[xTo, zTo] = true;

        Define define = new Define();
        GameObject prefab = GameObject.Find(define.GetTileName("EnemyHealthBar", xFrom, zFrom));
        AnimaEnemyHealthBar animaEnemyHealthBar = new AnimaEnemyHealthBar();
        animaEnemyHealthBar.MoveEnemyHealthBar(prefab, xFrom, xTo, zFrom, zTo);
    }

    
}
