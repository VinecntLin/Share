using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ControlPuck6 : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.isKinematic = true;
    }

    void Update()
    {
        // ERROR: You should not move a non-kinematic Rigidbody using Transform
        Vector3 position = MousePosition();
        // transform.position = position;
        if(Input.GetMouseButtonDown(0))
        {
            rb.AddForce(position,ForceMode.Impulse);
        }
    }

    private Vector3 MousePosition()
    {
        // use raycasting to turn mouse position into position on the board
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float t;
        plane.Raycast(ray, out t);
        return ray.GetPoint(t);
    }
}
