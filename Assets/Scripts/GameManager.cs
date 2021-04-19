using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Weapons
{
    Missile,
    Laser
}

public class GameManager : Singleton<GameManager>
{
    public Transform PlayerPrefab;
    public Vector2 StartPosition;
    public GameObject explosionPrefab;
    private Transform _player;
    private int _score;
    private int _level = 1;
    private int _health = 3;
    private bool _isPaused = false;
    private float distance;
    public List<Weapons> availableWeapons;
    private Weapons _currentWeapon;

    public Weapons CurrentWeapon
    {
        get => _currentWeapon;
        set
        {
            _currentWeapon = value;
            if (OnWeaponChange != null)
                OnWeaponChange(_currentWeapon);
        } 
    }

    public float Distance
    {
        get => distance;
        set
        {
            distance = value;
            Score += (int) distance / 5;
        }
    }

    public int Level 
    {
        get => _level;
        set
        {
            _level = value;
            if (OnLevelChange != null)
                OnLevelChange(_level);
        }
    }

    public bool IsPaused
    {
        get => _isPaused;
        set => _isPaused = value;
    }

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

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
                OnDeath();
            if (OnHealthChange != null)
                OnHealthChange(_health);
        }
    }

    public delegate void OnDeathDelegate();

    public event OnDeathDelegate OnDeath;
    
    public delegate void OnWeaponChangeDelegate(Weapons _weapon);

    public event OnWeaponChangeDelegate OnWeaponChange;

    public delegate void OnScoreChangeDelegate(int value);
    public event OnScoreChangeDelegate OnScoreChange;
    
    public delegate void OnHealthChangeDelegate(int value);
    public event OnHealthChangeDelegate OnHealthChange;

    public delegate void OnLevelChangeDelegate(int value);
    public event OnLevelChangeDelegate OnLevelChange;
    public Transform Player
    {
        get => _player;
        set => _player = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this);
        OnDeath += GameOver;
        Level = 1;
        Score = 0;
        availableWeapons = new List<Weapons>();
        availableWeapons.Add(Weapons.Missile);
        availableWeapons.Add(Weapons.Laser);
        CurrentWeapon = Weapons.Laser;
    }

    private void GameOver()
    {
        SwitchPause();
        
    }

    public void RestartGame()
    {
        
        ClearScene();
        //Player = Instantiate(PlayerPrefab, StartPosition, Quaternion.Euler(0,180,0));
        SwitchPause();
        SceneManager.LoadScene("Game");
    }

    private void ClearScene()
    {
        foreach (var gameObject in FindObjectsOfType<GameObject>())
        {
            if(gameObject != this.gameObject)
                Destroy(gameObject);
        }
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

    public void AddScore(int i)
    {
        Score += i;
    }

    public void SwitchPause()
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            Time.timeScale = 0;
            return;
        }

        Time.timeScale = 1;
        
    }
}
