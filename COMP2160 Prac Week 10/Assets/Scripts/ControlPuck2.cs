using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ControlPuck2 : MonoBehaviour
{
    // Start is called before the first frame update

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
        rb.position = position;
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
