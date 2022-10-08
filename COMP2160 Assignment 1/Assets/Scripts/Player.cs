using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Color hidden =Color.grey;
    private Color normal =Color.white;
    private bool isHidden;
    private BoxCollider2D PlayerCollider;

    [SerializeField]private float speed =2f;
    [SerializeField]private float ReduceSpeedRate =1000f;
    private float InCratePlayerSpeed;
    [SerializeField]private float rotationSpeed =1000f;
    
    
    private SpriteRenderer sprite;
    private bool StayInCrate;
    

    enum PlayerState{
        Hide,
        movement,
        slow,
        
    }
    private PlayerState playerState;
    //==========================================================================

    void Start()
    {   
        InCratePlayerSpeed =speed /ReduceSpeedRate;
        playerState =PlayerState.movement;
        PlayerCollider =GetComponent<BoxCollider2D>();
        PlayerCollider.isTrigger =true;
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        GameObject Crates = GameObject.FindWithTag("Crates");
        //control player movement
        float InputX =Input.GetAxis(InputAxes.Horizontal);
        float InputY =Input.GetAxis(InputAxes.Vertical);

        Vector2 movementDirection = new Vector2(InputX,InputY);
        float inputMagnitude =Mathf.Clamp01(movementDirection.magnitude);
        switch(playerState){
        //========= Player move ============================================
            case PlayerState.movement:
                sprite.color= normal;
                

                movementDirection.Normalize();
                transform.Translate(movementDirection* speed *inputMagnitude*Time.deltaTime, Space.World);

                //character turn with facing way
                if(movementDirection != Vector2.zero){
                    Quaternion toRotation =Quaternion.LookRotation(Vector3.forward,movementDirection);
                    transform.rotation =Quaternion.RotateTowards(transform.rotation,toRotation,rotationSpeed*Time.deltaTime);
                }

                //switch to hideMode==============
                if(Input.GetKeyDown(KeyCode.Space)){
                    playerState =PlayerState.Hide;
                    sprite.color= hidden;
                 }



                if(StayInCrate ==true){
                    Debug.Log("switch to SlowMode");
                    playerState =PlayerState.slow;
                 }
            break;
        
        //========= player hiding ============================================
        
            case PlayerState.Hide:
                PlayerCollider.isTrigger =false;

                if(Input.GetKeyUp(KeyCode.Space)){
                    playerState =PlayerState.movement;
                    PlayerCollider.isTrigger =true;
                }                
            break;
        
        //======== hiding in the crate ============================================
            case PlayerState.slow:
                sprite.color= normal;
                Debug.Log(InCratePlayerSpeed);
                movementDirection.Normalize();
                transform.Translate(movementDirection* InCratePlayerSpeed *inputMagnitude*Time.deltaTime, Space.World);

            //character turn with facing way
                if(movementDirection != Vector2.zero){
                    Quaternion toRotation =Quaternion.LookRotation(Vector3.forward,movementDirection);
                    transform.rotation =Quaternion.RotateTowards(transform.rotation,toRotation,rotationSpeed*Time.deltaTime);
                }
                 

                if(Input.GetKeyDown(KeyCode.Space)){
                    playerState =PlayerState.Hide;
                    sprite.color= hidden;
                 }

                if(StayInCrate ==false){
                    playerState =PlayerState.movement;
                }
            break;
        }
        
        

    } 
    //
    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Crates")){
            PlayerCollider.isTrigger =false;
            StayInCrate =true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Crates")){
            PlayerCollider.isTrigger =true;
            StayInCrate =false;

        }
    }
}
