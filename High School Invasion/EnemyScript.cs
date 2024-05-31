using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public EnemyHealth healthScript;

    public float physicalDamage;

    public int roomNumber;

    protected virtual void Start()
    {
        healthScript = GetComponentInParent<EnemyHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            if (other.GetComponent<RPGBullet>() == null)
            {
                healthScript.TakeHealth(other.GetComponent<Bullet>().damage);
            }
            else
            {
                healthScript.TakeHealth(other.GetComponent<RPGBullet>().damage);
            }
        }
    }

    private void OnDestroy()
    {
        FindObjectOfType<RoomDetection>().currentRoom.EnemyKilled(this);
    }
}



