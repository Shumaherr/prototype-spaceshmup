using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float fireRate = 3.5f; //Attack speed
    public GameObject missilePrefab;
    public Material laserMat;
    private bool _cooldown = false;
    private Transform _laser;
    private GameObject _laserInstance;

    private void Start()
    {
       
    }

    private void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transformPosition.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position = transformPosition;
        if (_laserInstance)
        {
            _laserInstance.GetComponent<LineRenderer>()
                .SetPosition(0, new Vector2(transform.position.x, transform.position.y));
            _laserInstance.GetComponent<LineRenderer>()
                .SetPosition(1, new Vector2(transform.position.x, transform.position.y + 100));
        }

        HandleInput();
       
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Fire1") && !_cooldown)
        {
            switch (GameManager.Instance.CurrentWeapon)
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

        if (Input.GetButtonDown("Fire2"))
        {
            SwitchWeapon();
        }
        if (Input.GetButtonUp("Fire1") && GameManager.Instance.CurrentWeapon == Weapons.Laser)
        {
            _cooldown = false;
            DeactivateLaser();
        }

        if (Input.GetButtonDown("Jump"))
        {
            GameManager.Instance.SwitchPause();
        }

    }

    private void SwitchWeapon()
    {
        int index = GameManager.Instance.availableWeapons.IndexOf(GameManager.Instance.CurrentWeapon);
        GameManager.Instance.CurrentWeapon = index == GameManager.Instance.availableWeapons.Count - 1 ? 
            GameManager.Instance.availableWeapons[0] : GameManager.Instance.availableWeapons[index + 1];
    }

    private void DeactivateLaser()
    {
        Destroy(_laserInstance);
    }

    private void ActivateLaser()
    {
        Vector2 playerPos = new Vector2(GameManager.Instance.Player.transform.position.x,
            GameManager.Instance.Player.transform.position.y);
        _laserInstance = new GameObject("LaserRay");
        //_laserInstance.transform.parent = this.transform;
        _laserInstance.AddComponent<FollowPlayer>();
        _laserInstance.tag = "PlayerShot";
        var line = _laserInstance.AddComponent<LineRenderer>();
        line.startColor = Color.green;
        line.endColor = Color.green;
        line.startWidth = 0.35f;
        line.endWidth = 0.35f;
        var laserCollider = _laserInstance.AddComponent<BoxCollider>();
        List<Vector2> colPos = new List<Vector2>();
        colPos.Add(playerPos);
        colPos.Add(new Vector2(playerPos.x, playerPos.y + 100));
        laserCollider.center = Vector3.zero;
        laserCollider.size = new Vector3(0.5f, 100);
        line.material = laserMat;
        line.SetPosition(0, new Vector2(transform.position.x, transform.position.y));
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