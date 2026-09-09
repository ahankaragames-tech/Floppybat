using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logicScript : MonoBehaviour
{
    public int playerScore;
    public Text playerScoreText;
    public GameObject gameoverPanel;
    public static bool isGameOver;
    public int highScore;
    public stalagSpawner spawner;

    private void Start()
    {
       highScore = PlayerPrefs.GetInt("highScore");
    }

    void Awake()
    {
        // Reset static flags as soon as the scene loads
        isGameOver = false; 
    }
    public void addScore()
    {
        playerScore = playerScore+ 1;
        playerScoreText.text = playerScore.ToString();
    }

    public void restartGame()
    {
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (spawner != null)
        {
            spawner.isSpawning = false;
        }
        isGameOver = true;
        
        gameoverPanel.SetActive(true);
        // 1. Get the previous record from disk
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (playerScore > savedHighScore)
        {
            PlayerPrefs.SetInt("highScore", playerScore);
            PlayerPrefs.Save();
            if (AudioManager.instance != null)
            {
                AudioManager.instance.playSFX(AudioManager.instance.highScoreSound);
            }
        }
        else
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.playSFX(AudioManager.instance.gameoverSound);
            }
        }
        
    }
}
