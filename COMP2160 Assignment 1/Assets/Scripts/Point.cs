using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    void Start()
    {   
        spriteRenderer =GetComponent<SpriteRenderer>();
        spriteRenderer.enabled =false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
