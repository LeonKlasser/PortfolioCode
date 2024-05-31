using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class DeathParticle : MonoBehaviour
{
    [SerializeField] private AudioSource explosion;
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private float timeTillDespawn;
    
    private CameraShake cameraShake;
    private Score score;
    
    private void Start()
    {
        // Add score to player
        score = FindObjectOfType<Score>();
        score.AddScore(100);

        // Play the explosion effect with camera shake
        explosion.Play();
        cameraShake = FindObjectOfType<CameraShake>();
        StartCoroutine(cameraShake.Shake(.15f, .02f));
        
        // Self-destruct in 1 second
        Destroy(this.gameObject, 1f);
        
        // Spawn the floating score text
        var scoreSpawned = Instantiate(floatingTextPrefab, transform.localPosition, Quaternion.identity);
        Destroy(scoreSpawned, timeTillDespawn);
    }
}
