using System.Collections;
using UnityEngine;

public class ParallaxScript : MonoBehaviour {
    private float startPos, length;
    public GameObject camera;
    public float paralaxEffect;
    public float startSpeed = 50.0f;

    public bool isMoving = true;
    private float _startPosY;

    void Start()
    {
        InvokeRepeating("AddDistance", 0,1 );
        InvokeRepeating("IncreaseSpeed", 10, 10);
        _startPosY = transform.position.y;
    }

    private void IncreaseSpeed()
    {
        startSpeed += 5;
    }

    private void AddDistance()
    {
        if (isMoving)
            GameManager.Instance.Distance += startSpeed;
    }

    void FixedUpdate()
    {
        if(isMoving)
        {
            Vector3 position = transform.position;
            
            float newPosY = (position.y - (Time.deltaTime * startSpeed)) >= -122.0f
                ? position.y - (Time.deltaTime * startSpeed)
                : _startPosY;
            Vector3 newPos = new Vector3(position.x, newPosY, position.z);
            transform.position = newPos;
        }
        
    }
}