using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    public float speed = 10f; //Move speed
    public float fireRate = 0.5f; //Attack speed
    public float health = 10; //Health points
    public int scoreRevard = 100; //Score for destroy
    public Transform missile; //Missle to shoot

    public Vector3 pos
    {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }

    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        Vector3 newPos = pos;
        newPos.x -= speed * Time.deltaTime;
        pos = newPos;
    }
}