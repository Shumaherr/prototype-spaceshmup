using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform PlayerPrefab;
    public Vector2 StartPosition;

    private Transform _player;

    public Transform Player
    {
        get => _player;
        set => _player = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        
    }

    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if(_player)
            return;
        _player = Instantiate(PlayerPrefab, StartPosition, Quaternion.Euler(0,180,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
