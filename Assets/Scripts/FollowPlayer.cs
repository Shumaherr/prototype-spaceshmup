using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private void Update()
    {
        transform.position = GameManager.Instance.Player.transform.position;
    }
}