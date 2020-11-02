using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 50.0f;
    public int damage = 1;
    private bool _isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _isPlayer = gameObject.tag.Contains("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        if(!_isPlayer)
            newPos.y -= moveSpeed * Time.deltaTime;
        else
            newPos.y += moveSpeed * Time.deltaTime;
        transform.position = newPos;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_isPlayer && other.gameObject.tag.Equals("Player"))
        {
            HitPlayer(damage);
        }
        else if(_isPlayer && other.gameObject.tag.Equals("Enemy"))
        {
            HitEnemy(other);
        }
    }

    private void HitEnemy(Collision other)
    {
        Destroy(other.gameObject);
        GameManager.Instance.AddScore(100);
    }

    private void HitPlayer(int f)
    {
        Destroy(gameObject);
        GameManager.Instance.Health -= f;
    }
}
