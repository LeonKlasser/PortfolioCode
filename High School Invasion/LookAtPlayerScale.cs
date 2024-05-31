using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerScale : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private Vector3 gunOffset;
    [SerializeField] private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player.transform.position.x < enemyTransform.position.x)
        {
            transform.localScale = new Vector3(.4f, .4f, .4f);
        }
        else
        {
            transform.localScale = new Vector3(-.4f, .4f, .4f);
        }

        transform.position = enemyTransform.position + gunOffset;
        transform.rotation = Quaternion.identity;
    }
}
