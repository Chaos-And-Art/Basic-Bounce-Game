using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSetUp : MonoBehaviour
{
    public static Rect GetScreenRect()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        return new Rect(cam.transform.position.x - width / 1.95f, cam.transform.position.y - height / 2, width, height);
    }
}
