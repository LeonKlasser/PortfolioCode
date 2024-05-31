using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSineWave : MonoBehaviour
{
    private float sinCenterY;
    [SerializeField] private float sinAmplitude = 2;
    [SerializeField] private float frequency = 0.5f;

    [SerializeField] private bool inverted = false;
    private void Start()
    {
        sinCenterY = transform.position.y;
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        float sin = Mathf.Sin(pos.x * frequency) * sinAmplitude;
        if (inverted)
        {
            sin *= -1;
        }
        pos.y = sinCenterY + sin;
        transform.position = pos;
    }
}
