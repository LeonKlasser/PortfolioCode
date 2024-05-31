using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private CameraShake cameraShake;        
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private FloatingHealthBar floatingHealthBar;
    
    public float Health { get; private set; }

    private SpriteRenderer sr;
    private PlayerController playerController;
    private Color startColor;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
        Health = maxHealth;
        startColor = sr.color;
        CheckHealth();
    }

    private void Update()
    {
        if (!playerController.isShooting)
        {
            AddHealth(1f * Time.deltaTime);
        }
    }

    public void AddHealth(float amount)
    {
        Health += amount;
        CheckHealth();
    }

    public void SetHealth(float amount)
    {
        Health = amount;
        CheckHealth();
    }
    
    public void TakeDamage()
    {
        hitSound.Play();
        StartCoroutine(cameraShake.Shake(0.10f, 0.015f));
        StartCoroutine(BlinkRed());
        Health--;
        CheckHealth();
    }
    
    private void CheckHealth()
    {
        if (Health > maxHealth)
            Health = maxHealth;
        if (Health < 0)
        {
            Health = 0;
            GameOver();
        }
        floatingHealthBar.UpdateHealthBar(Health, maxHealth);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    IEnumerator BlinkRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = startColor;
    }
}
