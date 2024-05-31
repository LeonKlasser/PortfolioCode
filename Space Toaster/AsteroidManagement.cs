using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManagement : MonoBehaviour
{
    [SerializeField] private bool instAsChild = false;
    [SerializeField] private GameObject parentObject;
    [SerializeField] private GameObject asteroid;
    [SerializeField] private float timeBetweenSpawns;
    private float spawnTimer;

    [SerializeField] private Vector2 spawnPositionX;
    [SerializeField] private Vector2 spawnPositionY;
    [SerializeField] private float zOffset = 0;

    void Start()
    {
        SpawnAsteroid();
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > timeBetweenSpawns){
            spawnTimer = 0;
            SpawnAsteroid();
        }
    }

    // Update is called once per frame
    void SpawnAsteroid(){
        float spawnPosX = Random.Range(spawnPositionX.x, spawnPositionX.y);
        float spawnPosY = Random.Range(spawnPositionY.x, spawnPositionY.y);
        //while (spawnPosX > 15 && spawnPosX < 18.2 
        //&& spawnPosY < 10.2 && spawnPosY > -0.6f){
        //    spawnPosX = Random.Range(15f, 18.5f);
        //    spawnPosY = Random.Range(10.5f, -0.8f);
        //}
        if (!instAsChild)
        {
            Instantiate(asteroid, new Vector3(spawnPosX, spawnPosY, zOffset), Quaternion.identity);
        }
        else if (instAsChild)
        {
            var child = Instantiate(asteroid, new Vector3(spawnPosX, spawnPosY, zOffset), Quaternion.identity);
            child.transform.parent = parentObject.transform;
        }
    }
}
