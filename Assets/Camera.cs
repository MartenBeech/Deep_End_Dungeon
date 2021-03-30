using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    public static Vector3 offset = new Vector3(0, 0, 0);
    public static GameObject Cam;

    public void Init()
    {
        Cam = GameObject.Find("Main Camera");
    }
    void Update()
    {
        Cam.transform.position = Player.player.transform.position + offset;
        Cam.transform.rotation = Player.player.transform.rotation;
    }
}
