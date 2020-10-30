using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Vector2 bounds;
    [Header("Set Dynamically")] 
    public float camWidth;
    public float camHeight;
    [FormerlySerializedAs("_camSize")] [Header("Set Dynamically")]
    private bool _outOfScreen;

    public bool OutOfScreen
    {
        get => _outOfScreen;
        set => _outOfScreen = value;
    }

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
        if (pos.x + _box.bounds.extents.x > bounds.x)
        {
            pos.x = bounds.x - _box.bounds.extents.x;
            _outOfScreen = true;
        }

        if (pos.x - _box.bounds.extents.x < -bounds.x)
        {
            pos.x = -bounds.x + _box.bounds.extents.x;
            _outOfScreen = true;
        }

        if (pos.y + _box.bounds.extents.y > bounds.y)
        {
            pos.y = bounds.y - _box.bounds.extents.y;
            _outOfScreen = true;
        }

        if (pos.y - _box.bounds.extents.y < -bounds.y)
        {
            pos.y = -bounds.y + _box.bounds.extents.y;
            _outOfScreen = true;
        }

        if (_outOfScreen && !gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
            return;
        }
        transform.position = pos;
    }
    
    void OnDrawGizmos()
    {
        // е
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}