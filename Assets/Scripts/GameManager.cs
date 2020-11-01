using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform PlayerPrefab;
    public Vector2 StartPosition;
    public GameObject explosionPrefab;
    private Transform _player;
    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            if (OnScoreChange != null)
                OnScoreChange(_score);
        }
    }

    public delegate void OnScoreChangeDelegate(int value);

    public event OnScoreChangeDelegate OnScoreChange;

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

    public void AddScore(int i)
    {
        Score += i;
    }
}
