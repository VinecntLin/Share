using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backdrop : MonoBehaviour
{
    public float speed = 5; // m/s
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void RewindTo(float x)
    {
        Vector3 pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }
}
