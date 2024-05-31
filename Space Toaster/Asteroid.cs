using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Quaternion qTo;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField] private ParticleSystem explodeEffect;
    float targetPosX;
    float targetPosY;
    [SerializeField] private float zOffset = 0;

    [SerializeField] Vector2 targetPositionX;
    [SerializeField] Vector2 targetPositionY;

    private void Start()
    {
        targetPosX = Random.Range(targetPositionX.x, targetPositionX.y);
        targetPosY = Random.Range(targetPositionY.x, targetPositionY.y);
        //while (targetPosX > -0.27 && targetPosX < 3.2 
        //&& targetPosY < 10.2 && targetPosY > -0.6f){
        //    targetPosX = Random.Range(-1f, 3.2f);
        //    targetPosY = Random.Range(10.5f, -0.8f);
        //}
        Destroy(gameObject, 20f);

        qTo = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosX, targetPosY, zOffset), speed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * rotateSpeed);
    }

    public void Explode()
    {
        Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
