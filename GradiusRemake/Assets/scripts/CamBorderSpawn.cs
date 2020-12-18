using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBorderSpawn: MonoBehaviour
{
    void Awake()
    {
        Camera cam = Camera.main;

        Vector2 bottomLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 topLeft = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        Vector2 topRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        Vector2 bottomRight = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

        EdgeCollider2D col = this.GetComponent<EdgeCollider2D>();

        Vector2[] edgePoints = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
        col.points = edgePoints;
    }

}
