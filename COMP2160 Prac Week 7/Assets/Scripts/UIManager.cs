using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Singleton
    static private UIManager instance;
    static public UIManager Instance 
    {
        get 
        {
            if (instance == null) 
            {
                Debug.LogError("There is not UIManager in the scene.");
            }            
            return instance;
        }
    }

    public Text scoreText;
    public Text livesText;
    public Text gameOverText;
    public string scoreFormat = "Score: {0}";
    public string livesFormat = "Lives: {0}";
    public string winText = "YOU WIN!!!";
    public string loseText = "YOU LOSE!!!";

    public GameObject gameOverPanel;

    void Awake() 
    {
        if (instance != null)
        {
            // there is already a UIManager in the scene, destory this one
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        scoreText.text = string.Format(scoreFormat, GameManager.Instance.Score);        
        livesText.text = string.Format(livesFormat, GameManager.Instance.LivesRemaining);        
    }

    public void ShowGameOver(bool win)
    {
        if (win)
        {
            gameOverText.text = winText;
        }
        else 
        {
            gameOverText.text = loseText;
        }
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        // reload this scene
        SceneManager.LoadScene(0);
        gameOverPanel.SetActive(false);
    }
}
