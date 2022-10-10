using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public Bullet bulletPrefab;
    private Transform gun;
    public float gunCooldown = 0.5f; // seconds
    private float gunTimer = 0;

    void Update()
    {
        gunTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Fire") && gunTimer <= 0)
        {
            gunTimer = gunCooldown;
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
        }        
    }

}
