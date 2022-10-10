using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float gravity = 10;
    public Vector3 velocity;

    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }
}
