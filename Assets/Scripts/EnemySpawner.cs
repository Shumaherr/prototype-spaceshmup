using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] public List<Transform> enemiesModels;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        int i = Random.Range(0, enemiesModels.Capacity);
        Instantiate(enemiesModels[i], GetRandomPos(), enemiesModels[i].rotation);
    }

    private Vector3 GetRandomPos()
    {
        int side = Random.Range(0, 1);
        return new Vector3(side < 0.5 ? -40:40, Random.Range(0, 40), 0);
    }
    
    
}
