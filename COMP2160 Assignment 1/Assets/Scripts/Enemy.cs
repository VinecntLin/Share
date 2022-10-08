using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [SerializeField]private GameObject pointA;
    [SerializeField]private GameObject pointB;
    //===============================================
    [SerializeField]private float movespeed =1.0f;
    [SerializeField]private float RotateSpeed = 20f;
    private Quaternion quaternion;

    //===============================================
    //[SerializeField]private PlayerMovement Player;
    [SerializeField]private GameObject visionBubble;

    // ========================================
    [SerializeField]private Color defaultColor;
    [SerializeField]private float defaultRadius =3.0f;

    // ========================================
    [SerializeField]private Color AlertColor;
    [SerializeField]private float AlertZoomOutSpeed =0.5f;

    // ========================================
    [SerializeField]private Color CautionColor;
    [SerializeField]private float CautionRadius =5.0f;

    //===========================================================
    [SerializeField] private Transform GunPoint;
    [SerializeField] private Bullet bulletPrefab;
    

    [SerializeField] private float ShootResetTimer =2f;
    //=================================================
    

    private float CurrentRadius;
    private bool FacePointB;
    //=======================================================
    enum EnemyState{
        patrolMode,
        Left,
        Right,
        cautionMode,
        AlertMode,

    };
    private EnemyState enemyState;
    //=====================================================
    private float Shoot_Time;

    
    private Vector2 EnemyCurrentPosition;
    private Vector2 PlyerPosition;
    private Vector2 PlyerLastPosition;
    
    // Use this for initialization
    void Start()
    {   
        //Set this enemy in point A to start
        transform.position=pointA.transform.position;
        //set visison bubble size  
        visionBubble.transform.localScale = new Vector3 (defaultRadius,defaultRadius,0);
        enemyState =EnemyState.Right;
        Shoot_Time =ShootResetTimer;
        
    }

    // Update is called once per frame
    void Update()
    {   
        GameObject Player = GameObject.FindWithTag("Player");
        Vector2 Start  = pointA.transform.position;
        Vector2 End    = pointB.transform.position;
        EnemyCurrentPosition = transform.position;

        PlyerPosition =Player.transform.position;
        Vector3 DirectionToPlayer = PlyerPosition -EnemyCurrentPosition;

        Vector3 DirectionToPointA =Start -EnemyCurrentPosition;
        Vector3 DirectionToPointB =End -EnemyCurrentPosition;
        //angle
        float FaceStart =Mathf.Atan2(DirectionToPointA.y,DirectionToPointA.x)* Mathf.Rad2Deg;
        float FaceEnd =Mathf.Atan2(DirectionToPointB.y,DirectionToPointB.x)* Mathf.Rad2Deg;
        
        
        float FacePlayer =Mathf.Atan2(DirectionToPlayer.y,DirectionToPlayer.x)* Mathf.Rad2Deg;
        
        switch(enemyState){
            case EnemyState.Right:
               
                FacePointB =true;
                //roate face point B and Move 
                quaternion = Quaternion.AngleAxis(FaceEnd, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * RotateSpeed);
               
                transform.position =Vector2.MoveTowards(transform.position,pointB.transform.position,movespeed*Time.deltaTime);
                


                //check guard is on Point B
                if(EnemyCurrentPosition ==End ){  
                   enemyState =EnemyState.Left;
                }

                //check player enter vision bubble;
                if(visionBubble.GetComponent<CircleCollider2D>().isTrigger ==true){
                    enemyState =EnemyState.cautionMode;
                        //Debug.Log("STOP, look player");
                };
            break;


            //=================================================
           case EnemyState.Left:
                
                //roate face and move to point A
                quaternion = Quaternion.AngleAxis(FaceStart, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * RotateSpeed);
                FacePointB =false;

                transform.position =Vector2.MoveTowards(transform.position,pointA.transform.position,movespeed*Time.deltaTime);
               

                
                //check is on the point A
                if(EnemyCurrentPosition ==Start ){  
                    enemyState =EnemyState.Right;
                    //transform.Rotate(new Vector3(0,0,180),Space.Self);
                }

                //check player enter vision bubble;
                if(visionBubble.GetComponent<CircleCollider2D>().isTrigger ==true){  
                    enemyState =EnemyState.cautionMode;
                    
                };
            break;


            //=================================================
            case EnemyState.cautionMode:
                //Face the player
                quaternion = Quaternion.AngleAxis(FacePlayer, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * RotateSpeed);
                visionBubble.transform.localScale= new Vector3 (CautionRadius,CautionRadius,0);
                visionBubble.GetComponent<SpriteRenderer>().color= CautionColor;

                Debug.Log("shoot");
                ShootTimer();

                
                if(visionBubble.GetComponent<CircleCollider2D>().isTrigger ==false){
                    enemyState =EnemyState.AlertMode;
                };
            break;

            //=================================================
            case EnemyState.AlertMode:
                visionBubble.GetComponent<SpriteRenderer>().color= AlertColor;
                // now vision bubble size smaller or equal patrol vision size 
                if (CurrentRadius <=defaultRadius){
                    CurrentRadius=CautionRadius;
                }


                if(visionBubble.transform.localScale.x >defaultRadius && visionBubble.transform.localScale.y >defaultRadius){                       
                    CurrentRadius -=AlertZoomOutSpeed *Time.deltaTime;
                    visionBubble.transform.localScale = new Vector3 (CurrentRadius,CurrentRadius,0);
                    if(visionBubble.GetComponent<CircleCollider2D>().isTrigger ==true){
                        enemyState =EnemyState.cautionMode;
                    };

                    if(visionBubble.transform.localScale.x <=defaultRadius && visionBubble.transform.localScale.y <=defaultRadius){
                        visionBubble.transform.localScale = new Vector3 (defaultRadius,defaultRadius,0);
                        visionBubble.GetComponent<SpriteRenderer>().color= defaultColor;
                        enemyState =EnemyState.patrolMode;
                    };
                };
            break;
            


            //=================================================
            case EnemyState.patrolMode:
                if(FacePointB ==true){
                    enemyState =EnemyState.Right;
                    //Debug.Log("contintue walking");
                }
                else{
                    enemyState =EnemyState.Left;
                } 
             break;
        }
    
    }



    void Shoot(){
        var bullet = Instantiate(bulletPrefab,GunPoint.position,transform.rotation);
        //Reset Timer
        Shoot_Time =ShootResetTimer;
        ShootTimer();
    }

    void ShootTimer(){
        
        if(Shoot_Time<=0){
            Debug.Log("Fire !!");
            Shoot();   
        }
        if(Shoot_Time>0){
            Shoot_Time -=Time.deltaTime;
            Debug.Log("Time :" + Shoot_Time);
        }
    }
}
