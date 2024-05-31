using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        transform.Translate(0, bulletSpeed * Time.deltaTime, 0);

        Vector2 screenSize = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        if (transform.position.x > screenSize.x || transform.position.x < -screenSize.x || transform.position.y > screenSize.y || transform.position.y < -screenSize.y)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        IDamagable iDamagable = col.gameObject.GetComponent<IDamagable>();
        iDamagable?.TakeDamage();
    }
}
