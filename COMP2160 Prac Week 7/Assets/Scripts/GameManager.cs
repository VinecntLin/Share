using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    // Singleton
    static private GameManager instance;
    static public GameManager Instance 
    {
        get 
        {
            if (instance == null)
            {
                Debug.LogError("There is no GameManager instance in the scene.");
            }
            return instance;
        }
    }

    private int score = 0;
    public int Score 
    {
        get
        {
            return score;
        }
    }
    public int scorePerMissile = 10;
    public int scorePerRadar = 50;
    public int scorePerPower = 500;

    public int numLives = 3;
    public float delayAfterDeath = 2; 
    private int livesRemaining;
    public int LivesRemaining
    {
        get
        {
            return livesRemaining;
        }
    }

    public Transform startingPosition;
    private Transform lastCheckpoint;

    private PlayerMove player;
    private Backdrop backdrop;

    //for Debug.log to check numof object has been destory and the time
    private int numMissileDestroy;
    private int numRadarDestroy;
    private int numPowerDestroy;
    private float timer;


    void Awake()
    {
        if (instance != null) 
        {
            // destroy duplicates
            Destroy(gameObject);            
        }
        else 
        {
            instance = this;
        }        
    }

    void Start()
    {
        AnalyticsEvent.GameStart();
        Debug.Log(AnalyticsEvent.GameStart());
        score = 0;
        livesRemaining = numLives;
        lastCheckpoint = startingPosition;

        // find the player and backdrop
        player = FindObjectOfType<PlayerMove>();
        backdrop = FindObjectOfType<Backdrop>();

        // AnalyticsEvent.GameStart("CheckPoints: " +lastCheckpoint + " Times: " +timer + " Player position: " + player); 
        Debug.Log("CheckPoints: " +lastCheckpoint + " Times: " +timer + " Player position: " + player);
    }

    void Update()
    {
        timer+=Time.deltaTime;
    }
    public void ScoreMissile()
    {
        score += scorePerMissile;
        numMissileDestroy++;
    }

    public void ScoreRadar()
    {
        score += scorePerRadar;
        numRadarDestroy++;
    }

    public void ScorePower()
    {
        score += scorePerPower;
        numPowerDestroy++;
        GameOver(true); // WIN
    }

    public void Die()
    {
        StartCoroutine(DieTimer());
    }

    private IEnumerator DieTimer()
    {
        livesRemaining--;
        yield return new WaitForSeconds(delayAfterDeath);

        if (livesRemaining == 0)
        {
            GameOver(false);    // LOSE
        }
        else 
        {
            backdrop.RewindTo(lastCheckpoint.position.x);
            player.transform.position = lastCheckpoint.position;
            player.Revive();
        }

    }

    public void Checkpoint(Transform checkpoint)
    {
        lastCheckpoint = checkpoint;
         Analytics.CustomEvent("CheckPoints", new Dictionary<string, object>
        {
            {"CheckPoint: ",checkpoint},
            {"Time: ", timer},
            {"Score: ", score},
            {"Num of Missiles Destroy: ", numMissileDestroy},
            {"Num of Radar Destroy: ", numRadarDestroy},
            {"Num of Power Destroy: ", numPowerDestroy}
        });
        Debug.Log("CheckPoints: "+ " " +checkpoint + " Times: "+ timer+ " Scores: " 
        + score + " Num of Missiles Destroy: " + numMissileDestroy + " Num of Radar Destroy: " + numRadarDestroy +
         " Num of Power Destroy: "+ numPowerDestroy);
        // numPowerDestroy =0;
        // numMissileDestroy=0;
        // numRadarDestroy=0;
    }

    public void GameOver(bool win)
    {
        backdrop.speed = 0;
        Analytics.CustomEvent("GameOver", new Dictionary<string, object>
        {
            {"Time: ", timer},
            {"Score: ", score},
            {"Num of Missiles Destroy: ", numMissileDestroy},
            {"Num of Radar Destroy: ", numRadarDestroy},
            {"Num of Power Destroy: ", numPowerDestroy}
        });

        Debug.Log("GameOver "+ " Times: "+ timer+ " Scores: " + score
        + " Num of Missiles Destroy: " + numMissileDestroy + " Num of Radar Destroy: " + numRadarDestroy +
         " Num of Power Destroy: "+ numPowerDestroy);
        UIManager.Instance.ShowGameOver(win);
        AnalyticsEvent.GameOver();
        Debug.Log(AnalyticsEvent.GameOver());
    }

}
