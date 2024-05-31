using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public struct EnemyInfo
{
    public GameObject Enemy;
    public Transform PathItTakes;
    public Enemy EnemyScript;

    public void MoveTo(Vector3 destination)
    {
        Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, destination,
            EnemyScript.EnemySpeed * Time.deltaTime);
    }
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesToSpawn;
    [SerializeField] private Transform spawnedEnemiesParent;
    
    public List<EnemyInfo> spawnedEnemies;

    
    
    private void Start()
    {
        spawnedEnemies = new List<EnemyInfo>();
    }

    private void Update()
    {
        foreach (var enemy in spawnedEnemies)
        {
            if (!enemy.EnemyScript)
            {
                spawnedEnemies.Remove(enemy);
                return;
            }
            var destinationWaypoint = enemy.EnemyScript.CurrentWaypoint + 1;
            if (destinationWaypoint > enemy.PathItTakes.childCount - 1)
            {
                destinationWaypoint = 0;
            }

            if (Vector3.Distance(enemy.Enemy.transform.position, enemy.PathItTakes.GetChild(destinationWaypoint).position) <= 0.01f)
            {
                if (destinationWaypoint == 0)
                    enemy.EnemyScript.CurrentWaypoint = 0;
                else
                    enemy.EnemyScript.CurrentWaypoint++;
            }


            enemy.MoveTo(enemy.PathItTakes.GetChild(destinationWaypoint).position);
        }
    }

    public void SpawnEnemy()
    {
        var newEnemy = new EnemyInfo();
        var enemyToSpawn = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
        
        var pathToTake = transform.Cast<Transform>().ToList();
        
        newEnemy.PathItTakes = pathToTake[Random.Range(0, pathToTake.Count)];
        
        var startPosition = newEnemy.PathItTakes.GetChild(0).position;
        
        var spawnedEnemy = Instantiate(enemyToSpawn, startPosition, Quaternion.identity, spawnedEnemiesParent);
        
        newEnemy.Enemy = spawnedEnemy;
        newEnemy.EnemyScript = spawnedEnemy.GetComponent<Enemy>();
        

        newEnemy.EnemyScript.CurrentWaypoint = 0;
        
        spawnedEnemies.Add(newEnemy);

    }
}
