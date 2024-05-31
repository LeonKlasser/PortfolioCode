using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockSpawner : MonoBehaviour
{
    [Header("Settings")]
    public Vector2 MaxSpawnBounds;
    [SerializeField] private int maxRocks;
    
    [Header("Rocks")]
    public List<RockScript> Rocks;
    [SerializeField] private GameObject _rock;
    
    private Camera _camera;

    
    
    private void Start()
    {
        Rocks = new List<RockScript>();
        while (Rocks.Count < maxRocks)
        {
            Vector2 spawnPosition;
            spawnPosition.x = Random.Range(-MaxSpawnBounds.x, MaxSpawnBounds.x);
            spawnPosition.y = Random.Range(-MaxSpawnBounds.y, MaxSpawnBounds.y);

            GameObject spawnedRock = Instantiate(_rock, spawnPosition, Quaternion.Euler(0,0, Random.Range(0, 359)), transform);
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            Rocks.Add(spawnedRock.GetComponent<RockScript>());
        }
    }

    private void Update()
    {
    }
}
