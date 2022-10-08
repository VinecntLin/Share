using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasures : MonoBehaviour
{   
    // Start is called before the first frame update
    private Color color;
    private int ScoreValue;
    private float size;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {   
        //if collider with play destory Treasure
        if(collider.gameObject.layer ==LayerMask.NameToLayer("Player")){

            size =transform.localScale.x + transform.localScale.y * 10;
            int ScoreValue = (int) size;
            ScoreKeeper.Instance.AddPoints(ScoreValue);
            //Debug.Log("Treasure ScoreValue :" + ScoreValue);
            Destroy(gameObject);
        }
    }
}
