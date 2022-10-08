using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text HighestScoreText;
    [SerializeField] private GameObject RestartPanel;
    [SerializeField] private GameObject player;
    private int keepingScore;
    private int score;
    private int highestScore;
    public static UIManager instance;
    // Start is called before the first frame update


    void Start()
    {   
        
        score =0;
        scoreText.text = "Score: $"+ score;
        RestartPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        score = ScoreKeeper.Instance.Score;
        scoreText.text = "Score: $"+ score;

        if(score>keepingScore)
        {   
            highestScore =ScoreKeeper.Instance.HighestScore;
            HighestScoreText.text = "Highest Score: $" + highestScore;

            
        }
        if(player == null){
            RestartPanel.SetActive(true);
        }
    }

    public void Restart(){
        SceneManager.LoadScene(0);
        RestartPanel.SetActive(false);
    }
}
