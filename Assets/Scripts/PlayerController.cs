using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

enum Weapons
{
    Missile,
    Laser
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float fireRate = 3.5f; //Attack speed
    public GameObject missilePrefab;
    public Material laserMat;
    private bool _cooldown = false;
    private List<Weapons> _availableWeapons;
    private Weapons currentWeapon;
    private GameObject _laser;
    private GameObject _laserInstance;
    private void Start()
    {
        _availableWeapons = new List<Weapons>();
        _availableWeapons.Add(Weapons.Missile);
        currentWeapon = Weapons.Laser;
    }

    private void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transformPosition.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position = transformPosition;
        if (Input.GetButtonDown("Fire1") && !_cooldown)
        {
            switch (currentWeapon)
            {
                case Weapons.Missile:
                    StartCoroutine("Fire");
                    break;
                case Weapons.Laser:
                    _cooldown = true;
                    ActivateLaser();
                    break;
            }
        }

        if (Input.GetButtonUp("Fire1") && currentWeapon == Weapons.Laser)
        {
            _cooldown = false;
            DeactivateLaser();
        }

        if (Input.GetButtonDown("Jump"))
        {
            GameManager.Instance.SwitchPause();
        }
    }

    private void DeactivateLaser()
    {
        Destroy(GetComponent<LineRenderer>());
    }

    private void ActivateLaser()
    {
        var line = GameManager.Instance.Player.gameObject.AddComponent<LineRenderer>();
        line.startColor = Color.green;
        line.endColor = Color.green;
        line.startWidth = 0.35f;
        line.endWidth = 0.35f;
        line.material = laserMat;
        line.SetPosition(0,new Vector2(transform.position.x, transform.position.y) );
        line.SetPosition(1, new Vector2(transform.position.x, transform.position.y + 100));
    }

    IEnumerator Fire()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
        _cooldown = true;
        yield return new WaitForSeconds(fireRate);
        _cooldown = false;
    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {
            var explosion = Instantiate(GameManager.Instance.explosionPrefab, other.contacts[1].point,
                Quaternion.identity);
            explosion.transform.parent = gameObject.transform;
            Vector3 size = other.gameObject.tag.Contains("Shot") ? new Vector3(2, 2, 2) : new Vector3(10, 10, 10);
            explosion.transform.localScale = size;
            explosion.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
            if (other.gameObject.GetComponent<Enemy>())
                GameManager.Instance.Health--;
        }
    }
}