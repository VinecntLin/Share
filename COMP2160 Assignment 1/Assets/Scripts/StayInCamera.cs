using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-8.71f,8.71f),
            Mathf.Clamp(transform.position.y,-4.83f,4.83f),transform.position.z);
    }
}