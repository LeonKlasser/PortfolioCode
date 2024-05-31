using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThree : MonoBehaviour
{
    [SerializeField] private Vector2 speedRange = new Vector2(1, 4);
    [SerializeField] private Vector2 spawnHeightRange = new Vector2(1, 9);
    [SerializeField] private float a = 0.1f;
    [SerializeField] private float b = 0;
    [SerializeField] private float c = 0;
    private bool movingUp;
    private float middleY = 5;
    private float x = 0;
    private float speed;

    private Vector3 lastPos;

    void Start()
    {
        float spawnHeight = Random.Range(spawnHeightRange.x, spawnHeightRange.y);

        if (spawnHeight > middleY)
        {
            movingUp = true;
        }
        else
        {
            movingUp = false;
        }
        speed = Random.Range(speedRange.x,speedRange.y);
        transform.position = new Vector3(transform.position.x, spawnHeight, transform.position.z);
        Destroy(this.gameObject, 10f);
    }

    void Update()
    {
        x += Time.deltaTime;
        float yPos = a*x*x + b*x + c;
        if (!movingUp)
        {
            yPos = -yPos;
        }
        transform.position = new Vector3(transform.position.x + (-speed * Time.deltaTime), yPos + middleY, transform.position.z);
        Vector3 deltaPos = (transform.position - lastPos).normalized;
        transform.forward = deltaPos;
        lastPos = transform.position;
    }
}
