using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    
    private void Start()
    {

    }

    private void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.x += Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        transformPosition.y += Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        transform.position = transformPosition;
    }
}