using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [SerializeField]private float BulletSpeed =4f;
    [SerializeField]private float BulletExistTime =10f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(BulletSpeed * Vector2.right * Time.deltaTime);
        if(BulletExistTime >0){
            BulletExistTime -=Time.deltaTime;
        }
        if(BulletExistTime <=0){
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {   
        //if collider with play, destory player
        if(collider.gameObject.layer ==LayerMask.NameToLayer("Player")){
            Destroy(collider.gameObject);
        }
        Debug.Log("Bullet Hit " + collider.name);
        //destory Bullet when hit something
        Destroy(gameObject);
    }

}
