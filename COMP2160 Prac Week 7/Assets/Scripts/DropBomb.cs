using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public Bomb bombPrefab;
    public float dropSpeed = 1.0f;

    public float bombCooldown = 0.5f; // seconds
    private float bombTimer = 0;

    void Update()
    {
        bombTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Bomb") && bombTimer <= 0)
        {
            bombTimer = bombCooldown;
            Bomb bomb = Instantiate(bombPrefab);
            bomb.transform.position = transform.position;
            bomb.velocity.y = -dropSpeed;
        }        
    }

}
