using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    static private ScoreKeeper instance;
    private int score;
    private int highestScore;
    [SerializeField] private int TreasureScore;

    static public ScoreKeeper Instance{
        get
        {
            if(instance == null){
                Debug.LogError("No Scorekeeper in the scene.");
                
            } 
            return instance;
        }
    }

    void OnAwake(){
        if(instance !=null){
            Debug.LogError("More that one Scorekeeper in the scene.");
            Destroy(gameObject);
        }   
        instance = this; 
    }
    
    public int Score{
        get
        {   
            return score;
        }
    }

    public int HighestScore{
        get
        {   
            if(score>highestScore){
                highestScore=score;
            }
            return highestScore;
        }
    }


    void Start()
    {   
        if(instance ==null){
            instance =this;
        }
    }

    // Update is called once per frame
    public void AddPoints(int ScoreValue){
        score += TreasureScore*ScoreValue;
    }
}
