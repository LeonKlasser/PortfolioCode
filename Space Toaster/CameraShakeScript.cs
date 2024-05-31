using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour
{
    [SerializeField] private float shakeAmount = 0.7f;

    [SerializeField] private float shakeDuration = 0.5f;

    [SerializeField] private bool autoShake = false;

    private float shakeTimer = 0f;

    Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            Vector3 newPos = originalPos + Random.insideUnitSphere * shakeAmount;

            transform.position = newPos;

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            transform.position = originalPos;
        }

        if (autoShake)
        {
            Shake();
        }
    }

    public void Shake()
    {
        shakeTimer = shakeDuration;
    }

    public void SetHeavyShake()
    {
        shakeAmount = 0.02f;
    }
}
