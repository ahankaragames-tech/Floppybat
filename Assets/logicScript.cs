using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logicScript : MonoBehaviour
{
    public int playerScore;
    public Text playerScoreText;
    public Text gameoverText;
    public GameObject gameoverPanel;
    public static bool isGameOver;
    public int highScore;
    public stalagSpawner spawner;

    private void Start()
    {
        // Load the saved high score ONCE on start using consistent key "HighScore"
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Awake()
    {
        // Reset static flags as soon as the scene loads
        isGameOver = false; 
        // testing variable PlayerPrefs.DeleteAll();
    }
    public void addScore()
    {
        if (isGameOver) return;
        playerScore++;
        playerScoreText.text = playerScore.ToString();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (spawner != null)
        {
            spawner.isSpawning = false;
        }
        isGameOver = true;
    
        Debug.Log($"[GameOver Check] Player Score: {playerScore} | Stored High Score: {highScore}");
        
        // 2. Evaluate score and update UI text BEFORE turning on the panel
        if (playerScore >= highScore)
        {
            // Save to disk AND update local variable
            PlayerPrefs.SetInt("highScore", playerScore);
            PlayerPrefs.Save();
            highScore = playerScore;

            if (gameoverText != null)
            {
                gameoverText.text = "High score : " + playerScore + " !";
            }
    
            if (AudioManager.instance != null)
            {
                AudioManager.instance.playSFX(AudioManager.instance.highScoreSound);
            }
        }
        else
        {
            if (gameoverText != null)
            {
                gameoverText.text = "Game Over";
            }

            if (AudioManager.instance != null)
            {
                AudioManager.instance.playSFX(AudioManager.instance.gameoverSound);
            }
        }

        // 3. NOW enable the panel with the correct text already populated
        gameoverPanel.SetActive(true);
    }
}
