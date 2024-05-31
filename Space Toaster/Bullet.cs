using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction = new Vector3(1,0);
    [SerializeField] private float speed = 10;

    [SerializeField] private Vector3 velocity;

    [SerializeField] private bool bigBullet = false;
    [SerializeField] private Transform childScale;
    [SerializeField] private float growSpeed = 5;
    private void Start()
    {
        Destroy(gameObject, 3);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        velocity = direction * speed;
        if (bigBullet)
        {
            if (transform.localScale.x < 5)
            {
                transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime * growSpeed;
                childScale.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime * growSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        //Vector3 pos = transform.position;
        //pos += velocity * Time.fixedDeltaTime;
        //transform.position = pos;
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
        //transform.LookAt(transform.position + velocity);
    }
}
