using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
   public float speed;
    public float fireRate = 3.5f; //Attack speed
    public GameObject missilePrefab;
    private bool _cooldown = false;

    private void Start()
    {

    }

    private void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transformPosition.y += Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position = transformPosition;
        if (Input.GetButtonDown("Fire1") && !_cooldown)
        {
            StartCoroutine("Fire");
        }

        if (Input.GetButtonDown("Jump"))
        {
            GameManager.Instance.SwitchPause();
        }
    }

    IEnumerator Fire()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
        _cooldown = true;
        yield return new WaitForSeconds(fireRate);
        _cooldown = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Enemy"))
        {
            var explosion = Instantiate(GameManager.Instance.explosionPrefab, other.contacts[1].point, Quaternion.identity);
            explosion.transform.parent = gameObject.transform;
            Vector3 size = other.gameObject.tag.Contains("Shot") ? new Vector3(2, 2, 2) : new Vector3(10, 10, 10);
            explosion.transform.localScale = size;
                explosion.GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
        }
    }
}