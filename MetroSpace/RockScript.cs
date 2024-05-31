using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Screen = UnityEngine.Device.Screen;

public class RockScript : MonoBehaviour
{
    [SerializeField] private Vector2 rockSpeeds;
    [SerializeField] private Vector2 yAdditionAmount;

    private float yAdditions;
    private float rockSpeed;
    private Vector2 screenSize;
    
    private void Start()
    {
        rockSpeed = Random.Range(rockSpeeds.x, rockSpeeds.y);
        yAdditions = Random.Range(yAdditionAmount.x, yAdditionAmount.y);
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    private void Update()
    {
        transform.Translate(new Vector2(rockSpeed * Time.deltaTime, yAdditions * Time.deltaTime));



        if (transform.position.x > 9.5f) 
            transform.Translate(new Vector2(-21, 0), Space.World);
        if (transform.position.x < -9.5f) 
            transform.Translate(new Vector2(21, 0), Space.World);
        
        if (transform.position.y > 6f) 
            transform.Translate(new Vector2(0, -11.6f), Space.World);
        if (transform.position.y < -6f) 
            transform.Translate(new Vector2(0, 11.6f), Space.World);
    }
    
}
