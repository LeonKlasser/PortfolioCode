using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class Enemy : MonoBehaviour, IDamagable
{
    public float EnemySpeed;
    public int CurrentWaypoint;

    [SerializeField] private int enemyHealth;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private float shootCooldown;
    [SerializeField] private GameObject bullet;

    private SpriteRenderer sr;
    private Transform player;
    private Object deathParticle;
    private Color enemyColor;
    private float timeSinceShot;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        deathParticle = Resources.Load("Death Particle");
        enemyColor = sr.color;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        timeSinceShot += Time.deltaTime;

        if (timeSinceShot > shootCooldown)
        {
            Shoot();
            timeSinceShot = 0;
        }
        
        transform.LookAt(player.position, Vector3.forward);
        transform.rotation = Quaternion.Euler(0, 0, -transform.eulerAngles.z);

    }

    public void TakeDamage()
    {
        // Take one health from enemy
        enemyHealth--;
        hitSound.Play();
        StartCoroutine(BlinkRed());
            
        // If enemy has no health, DIE!
        if (enemyHealth == 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    IEnumerator BlinkRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = enemyColor;
    }

    
}
