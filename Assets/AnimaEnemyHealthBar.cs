using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaEnemyHealthBar : MonoBehaviour
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
                Define define = new Define();
                this.name = define.GetTileName("EnemyHealthBar", xEnd, zEnd);
            }
        }

        this.transform.rotation = new Quaternion(Player.player.transform.rotation.x, Player.player.transform.rotation.y, Player.player.transform.rotation.z, Player.player.transform.rotation.w);
    }

    public void SummonEnemyHealthBar(int x, int z)
    {
        prefab = Resources.Load<GameObject>("Assets/EnemyHealthBar");
        parent = GameObject.Find("EnemyHealthBars");

        GameObject newTile = Instantiate(prefab, new Vector3(x, Player.yPos * 2.5f, z), new Quaternion(0, 0, 0, 0), parent.transform);
        Define define = new Define();
        newTile.name = define.GetTileName("EnemyHealthBar", x, z);
    }

    public void MoveEnemyHealthBar(GameObject gameObject, int xFrom, int xTo, int zFrom, int zTo)
    {
        gameObject.GetComponentInChildren<AnimaEnemyHealthBar>().xStart = xFrom;
        gameObject.GetComponentInChildren<AnimaEnemyHealthBar>().xEnd = xTo;
        gameObject.GetComponentInChildren<AnimaEnemyHealthBar>().zStart = zFrom;
        gameObject.GetComponentInChildren<AnimaEnemyHealthBar>().zEnd = zTo;
        gameObject.GetComponentInChildren<AnimaEnemyHealthBar>().moveCounter += 1 / Game.SPEED;
    }
}
