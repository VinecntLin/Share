using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    private Rect flightRect;

    public Explosion explosionPrefab;

    public SpriteRenderer sprite;

    void Start()
    {
        // get the camera rect
        // note this assumes the window is aligned with the world x/y axes
        Camera camera = Camera.main;
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));

        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        // shrink the box by the size of the sprite
        flightRect.xMin = bottomLeft.x + sprite.bounds.extents.x;
        flightRect.xMax = topRight.x - sprite.bounds.extents.x;
        flightRect.yMin = bottomLeft.y + sprite.bounds.extents.y;
        flightRect.yMax = topRight.y - sprite.bounds.extents.y;
    }

    void Update()
    {
        float vx = Input.GetAxis("Horizontal");
        float vy = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(vx, vy, 0) * speed;
        transform.Translate(velocity * Time.deltaTime);
        // clamp to stay on screen
        transform.localPosition = flightRect.Clamp(transform.localPosition);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        // die if colliding with terrain, missles or radars
        if (!Layers.Instance.checkpoint.Contains(other))
        {
            Die();
        }
        if(collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Debug.Log("Player died from hitting the terrain");
        }
        if(collider.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            Debug.Log("Player died from the missile");
        }
        if(collider.gameObject.layer == LayerMask.NameToLayer("Radar"))
        {
            Debug.Log("Player died from hitting the radar");
        }
        if(collider.gameObject.layer == LayerMask.NameToLayer("Power"))
        {
            Debug.Log("Player had won the level");
        }
    }

    private void Die()
    {
        // hide the ship
        gameObject.SetActive(false);

        // create an explosion and wait for it to complete before telling the GameManager
        Explosion explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;

        // tell the GameManager
        GameManager.Instance.Die();
    }

    public void Revive()
    {
        gameObject.SetActive(true);
    }
}
