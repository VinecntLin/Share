using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //When enemy collider with player
    void OnTriggerEnter2D(Collider2D collider)
    {   
        //if collider with play layer, destory player
        if(collider.gameObject.layer ==LayerMask.NameToLayer("Player")){
            Destroy(collider.gameObject);
        }
    }
}
