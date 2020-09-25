using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    public float speed = 30;

    private Vector3 _nextPos;
    private Vector3 _input;
    private Animator _animator;

    // Update is called once per frame
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        HandleAnimation();
        HandleMove();
    }

    private void HandleMove()
    {
        transform.position = _nextPos;
    }

    private void HandleAnimation()
    {
        _animator.SetFloat("Input", _input.x);
    }

    void HandleInput()
    {
        _input.x = Input.GetAxis("Horizontal"); 
        _input.y = Input.GetAxis("Vertical"); 

        _nextPos.x += _input.x * speed * Time.deltaTime;
        _nextPos.y += _input.y * speed * Time.deltaTime;
    }
}
