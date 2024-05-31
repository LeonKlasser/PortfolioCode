using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D rb;
    private new Camera camera;
    private EnemySpawner enemySpawner;
    private Transform origin;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        camera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        origin = GameObject.FindGameObjectWithTag("Player").transform;
        FindNearestTarget();
    }
    
    private void FixedUpdate () {
        Vector2 screenSize = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if (transform.position.x > screenSize.x || transform.position.x < -screenSize.x || transform.position.y > screenSize.y || transform.position.y < -screenSize.y)
        {
            Destroy(this.gameObject);
        }
        if (!target)
        {
            FindNearestTarget();

            return;
        }

        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotationSpeed;

        rb.velocity = transform.up * speed;
    }

    private void FindNearestTarget()
    {
        int nearestDistanceIndex = 0;
        float nearestDistance = 100;
        Vector2 screenSize = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Transform selectedEnemy = transform;
        for (int i = 0; i < enemySpawner.spawnedEnemies.Count; i++)
        {
            Transform enemyTransform;
            if (enemySpawner.spawnedEnemies[i].Enemy)
                enemyTransform = enemySpawner.spawnedEnemies[i].Enemy.transform;
            else 
                return;

            var distanceFromPlayer = Vector2.Distance(origin.position, enemyTransform.position);


            if (distanceFromPlayer < nearestDistance)
            {
                nearestDistance = distanceFromPlayer;
                nearestDistanceIndex = i;
                selectedEnemy = enemyTransform;
            }

        }

        if (!(selectedEnemy.position.y < screenSize.x && selectedEnemy.position.x > -screenSize.x && selectedEnemy.position.y < screenSize.y && selectedEnemy.position.y > -screenSize.y))
            nearestDistanceIndex = -1;
            

        if (enemySpawner.spawnedEnemies.Count > nearestDistanceIndex && nearestDistanceIndex >= 0) 
            target = enemySpawner.spawnedEnemies[nearestDistanceIndex].Enemy.transform;
        else
            transform.Translate(0, speed * Time.deltaTime, 0);
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        IDamagable iDamagable = col.gameObject.GetComponent<IDamagable>();
        iDamagable?.TakeDamage();
    }
}
