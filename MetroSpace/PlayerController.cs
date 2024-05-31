using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("isMoving")] public bool isShooting;
    
    [SerializeField] private float shootingMoveSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject homingBullet;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float homingShootCooldown;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private float dashCooldown = 1f;
    
    private Rigidbody2D rb2d;
    private new Camera camera;
    private Transform shootPoint;
    private CameraShake cameraShake;
    
    private float currentSpeed;
    private float sinceLastShot;
    private float sinceLastHomingShot;
    private bool isDashing = false;
    private float dashTime;
    

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        camera = Camera.main;
        cameraShake = FindObjectOfType<CameraShake>();

        foreach (Transform child in transform)
        {
            if (child.name == "Shootpoint")
                shootPoint = child;
        }
    }

    private void Update()
    {
        HandleDash();
        
        // Move forwards        
        if (Input.GetKey(KeyCode.W))
            Move(MoveDirection.Forward);
        
        // Move backwards
        if (Input.GetKey(KeyCode.S))
            Move(MoveDirection.Backward);
        
        // Shoot a bullet
        if (Input.GetMouseButton(0))
        {
            Shoot();
            ShootHomingMissile();
            isShooting = true;
            if (!isDashing)
                currentSpeed = shootingMoveSpeed;
        }
        else
        {
            isShooting = false;
            if (!isDashing)
                currentSpeed = moveSpeed;
        }
        
        // Bullet cooldown
        sinceLastShot += Time.deltaTime;
        sinceLastHomingShot += Time.deltaTime;
        
        FaceTheMouse();
    }

    private void FaceTheMouse()
    {
        transform.LookAt(camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);        transform.rotation = Quaternion.Euler(0, 0, -transform.eulerAngles.z);

    }


    private void HandleDash()
    {
        dashTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTime > dashCooldown)
        {
            StartCoroutine(Dash());
            dashTime = 0;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        currentSpeed = moveSpeed * 8;

        yield return new WaitForSeconds(0.05f);
        currentSpeed = moveSpeed;
        isDashing = false;
        yield return null;
    }

    private void Move(MoveDirection moveDirection)
    {
        if (moveDirection == MoveDirection.Forward)
            // Apply force to go forwards
            rb2d.AddRelativeForce(Vector2.up * (currentSpeed * Time.deltaTime), ForceMode2D.Force);
        
        if (moveDirection == MoveDirection.Backward)
            // Apply force to go backwards
            rb2d.AddRelativeForce(Vector2.down * (currentSpeed * Time.deltaTime), ForceMode2D.Force);
    }

    private void Shoot()
    {
        StartCoroutine(cameraShake.Shake(.02f, .0015f));
        if (sinceLastShot > shootCooldown)
        {
            Instantiate(bullet, shootPoint.position, transform.rotation);
            sinceLastShot = 0;
            shootSound.Play();
        }
    }    
    
    private void ShootHomingMissile()
    {
        if (sinceLastHomingShot > homingShootCooldown)
        {
            Instantiate(homingBullet, shootPoint.position, transform.rotation);
            sinceLastHomingShot = 0;
        }
    }
    
}