using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    public float speed = 10f; //Move speed
    public float fireRate = 3.5f; //Attack speed
    public float health = 10; //Health points
    public int scoreRevard = 100; //Score for destroy
    public Transform missile; //Missle to shoot
    private bool _cooldown;
    private BoundsCheck _bounds;
    private bool _moveToPos = true;

    private void Start()
    {
        _bounds = GetComponent<BoundsCheck>();
    }

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
        Instantiate(missile, pos, missile.rotation);
        _cooldown = false;
    }

    private void HandleMove()
    {
        Random r = new Random();
        Vector3 targetPos = GameManager.Instance.Player.position;
        Vector3 newPos = pos;
        float deltaTime = Time.deltaTime;
        if (newPos.x < 0 + r.NextDouble() && _moveToPos)
            newPos.x += speed * deltaTime;
        else
        {
            _moveToPos = false;
            if (pos.x < targetPos.x)
                newPos.x += speed * deltaTime;
            if (pos.x > targetPos.x)
                newPos.x -= speed * deltaTime;
            newPos.y -= speed * deltaTime;
        }
        pos = newPos;
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerShot"))
        {
            HitEnemy(other.gameObject.name);
        }
    }

    private void HitEnemy(string gameObjectName)
    {
        if (gameObjectName.Contains("Laser"))
        {
            //TODO Implement damage in time
            Destroy(gameObject);
        }
        if (gameObjectName.Contains("Missile"))
        {
            //TODO Implement constant damage
            Destroy(gameObject);
        }
        
    }

    private void OnDestroy()
    {
        var explosion = Instantiate(GameManager.Instance.explosionPrefab, transform.position,
            Quaternion.identity);
        explosion.transform.parent = gameObject.transform;
        Vector3 size = new Vector3(10, 10, 10);
        explosion.transform.localScale = size;
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
    }
}