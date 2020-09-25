using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoundsCheck : MonoBehaviour
{
    [Header("Set Dynamically")] 
    public float camWidth;
    public float camHeight;
    [FormerlySerializedAs("_camSize")] [Header("Set Dynamically")]
    public Vector2 camSize;

    private BoxCollider _box;

    void Awake()
    {
        _box = GetComponent<BoxCollider>();
        camHeight = Camera.main.orthographicSize - _box.size.y;
        camWidth = camHeight * Camera.main.aspect - _box.size.x;
        Camera cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        if (pos.x > camWidth)
        {
            pos.x = camWidth;
        }

        if (pos.x < -camWidth)
        {
            pos.x = -camWidth;
        }

        if (pos.y > camHeight)
        {
            pos.y = camHeight;
        }

        if (pos.y < -camHeight)
        {
            pos.y = -camHeight;
        }

        transform.position = pos;
    }

// Рисует границы в панели Scene (Сцена) с помощью OnDrawGizmos()
    void OnDrawGizmos()
    {
        // е
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}