using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;

    private void Start()
    {
        //Destroy(this, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (!collision.gameObject.CompareTag("EnemyBullet"))
            {
                if (!collision.gameObject.CompareTag("Money"))
                {
                    if (!collision.gameObject.CompareTag("DoorPiece"))
                    {
                        if (!collision.gameObject.CompareTag("Enemy"))
                        {
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerBullet"))
        {
            if (!other.gameObject.CompareTag("EnemyBullet"))
            {
                if (!other.gameObject.CompareTag("Money"))
                {
                    if (!other.gameObject.CompareTag("DoorPiece"))
                    {
                        if (!other.gameObject.CompareTag("Enemy"))
                        {
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }
}
