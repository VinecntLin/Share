using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crates : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.layer ==LayerMask.NameToLayer("Player")){
            GetComponent<BoxCollider2D>().isTrigger =true;
            Debug.Log("collider with :" + collider.name);
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.layer ==LayerMask.NameToLayer("Player")){
            GetComponent<BoxCollider2D>().isTrigger =false;
            Debug.Log(collider.name + "Leave");
        }
    }


}
