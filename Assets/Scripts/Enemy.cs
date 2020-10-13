using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    public float speed = 10f; //Move speed
    public float fireRate = 3.5f; //Attack speed
    public float health = 10; //Health points
    public int scoreRevard = 100; //Score for destroy
    public Transform missile; //Missle to shoot
    private bool _cooldown;

    public Vector3 pos
    {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }

    void Update()
    {
        HandleMove();
        HandleFire();
    }

    private void HandleFire()
    {
        Vector3 targetPos = GameManager.Instance.Player.position;
        Vector3 newPos = pos;
        if (Math.Abs(targetPos.x - newPos.x) < 1.0f && !_cooldown)
            StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        _cooldown = true;
        yield return new WaitForSeconds(fireRate);
        Instantiate(missile, pos, Quaternion.identity);
        _cooldown = false;
    }

    public void HandleMove()
    {
        Vector3 targetPos = GameManager.Instance.Player.position;
        Vector3 newPos = pos;
        if(pos.x < targetPos.x)
            newPos.x += speed * Time.deltaTime;
        if(pos.x > targetPos.x)
            newPos.x -= speed * Time.deltaTime;
        pos = newPos;
    }
}