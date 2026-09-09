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
        isGameOver = true;
        if (spawner != null)
        {
            spawner.isSpawning = false;
        }
        
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        if (playerScore > currentHighScore)
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
        gameoverPanel.SetActive(true);
    }
}
