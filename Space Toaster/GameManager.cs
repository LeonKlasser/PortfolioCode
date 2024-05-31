using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab; 
    [SerializeField] private Vector3 spawnLocation;
    [SerializeField] private Vector2 spawnRate;
    [SerializeField] private float enemySpawnDistance;
    [SerializeField] private float minToDoubleSpawn;
    private float timeSinceLastSpawn;

    void Start()
    {
    }

    void Update()
    {
        if (!LevelController.instance.bossLevel)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= Random.Range(spawnRate.x, spawnRate.y))
            {
                spawnEnemy();
                if (Random.Range(0.1f, 15f) < minToDoubleSpawn)
                {
                    spawnEnemy();
                }
            }
        }
    }

    public void NewLevel()
    {
        if (spawnRate.x > 0.3f)
        {
            spawnRate.x -= 0.2f;
            spawnRate.y -= 0.2f;
        }
        if (minToDoubleSpawn < 14)
        {
            minToDoubleSpawn += 0.2f;
        }

    }

    public void ResetSpawnRate()
    {
        spawnRate.x = 4;
        spawnRate.y = 6;
    }

    private void spawnEnemy()
    {
        spawnLocation = new Vector3(19, Random.Range(9.2f, 0), 0);
        timeSinceLastSpawn = 0;
        GameObject currentPrefab = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        if (currentPrefab == enemyPrefab[0])
        {
            for (int i = 0; i < Random.Range(3, 7); i++)
            {
                Instantiate(currentPrefab, new Vector3(spawnLocation.x + i * 1.65f, spawnLocation.y, spawnLocation.z), Quaternion.identity);
            }
        } else {
            Instantiate(currentPrefab, new Vector3(spawnLocation.x, spawnLocation.y, spawnLocation.z), Quaternion.Euler(0, -90, 0));
        }
        spawnLocation.x += enemySpawnDistance;
    }
}
