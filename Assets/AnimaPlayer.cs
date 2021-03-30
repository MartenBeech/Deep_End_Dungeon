using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimaPlayer : MonoBehaviour
{
    private static int xStart;
    private static int zStart;
    private static int xEnd;
    private static int zEnd;
    private static float angleFrom;
    private static float angleTo;

    private static float moveCounter = 0f;
    private static float rotateCounter = 0f;

    void Update()
    {
        if (moveCounter > 0)
        {
            Vector3 dir = new Vector3(xEnd, Player.yPos, zEnd) - new Vector3(xStart, Player.yPos, zStart);
            float dist = Mathf.Sqrt(
                Mathf.Pow(xEnd - xStart, 2) +
                Mathf.Pow(zEnd - zStart, 2));
            Player.player.transform.position += (dir.normalized * dist * (Time.deltaTime) * Game.SPEED);
            moveCounter -= Time.deltaTime;

            if (moveCounter <= 0)
            {
                Player player = new Player();
                player.MovePlayer(xEnd, zEnd);
            }
        }

        if (rotateCounter > 0)
        {
            float offset = rotateCounter * Game.SPEED;
            float degrees = (angleTo * (1 - offset)) + angleFrom * offset;

            Player.player.transform.rotation = new Quaternion(Player.player.transform.rotation.x, Mathf.Sin(degrees / 2), Player.player.transform.rotation.z, Mathf.Cos(degrees / 2));
            rotateCounter -= Time.deltaTime;

            if (rotateCounter <= 0)
            {
                Player player = new Player();
                player.RotatePlayer(angleTo);
            }
        }
    }

    public void MovePlayer(int xTo, int zTo)
    {
        xStart = Player.xPos;
        zStart = Player.zPos;
        xEnd = xTo;
        zEnd = zTo;
        moveCounter = 1 / Game.SPEED;
    }

    public void RotatePlayer(float degrees)
    {
        angleFrom = Player.angle;
        angleTo = angleFrom + (degrees / 360 * 2 * Mathf.PI);
        rotateCounter = 1 / Game.SPEED;
    }
}
