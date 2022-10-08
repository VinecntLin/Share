using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionBubble : MonoBehaviour
{   
    [SerializeField] private GameObject Player;
    void Start()
    {   

    }


    void Update()
    {
        if(Player.GetComponent<BoxCollider2D>().isTrigger ==false){
            GetComponentInParent<CircleCollider2D>().isTrigger=false;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {   
        //if collider with play destory 
        if(collider.gameObject.layer ==LayerMask.NameToLayer("Player")  && Player.GetComponent<BoxCollider2D>().isTrigger ==true){
            GetComponent<CircleCollider2D>().isTrigger=true;
            //Debug.Log(collider.name +" entry Vision");
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.layer ==LayerMask.NameToLayer("Player")|| Player.GetComponent<BoxCollider2D>().isTrigger ==true){
            GetComponentInParent<CircleCollider2D>().isTrigger=false;
        }
        //Debug.Log(collider.name +" is exit Collide");
    }


}
