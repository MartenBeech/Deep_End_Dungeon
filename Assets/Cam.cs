using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cam : MonoBehaviour
{
    public static Vector3 offset = new Vector3(0, 0, 0);
    public static GameObject cam;

    public void Init()
    {
        cam = GameObject.Find("Main Camera");
    }
    void Update()
    {
        cam.transform.position = Player.player.transform.position + offset;
        cam.transform.rotation = Player.player.transform.rotation;
    }
}
