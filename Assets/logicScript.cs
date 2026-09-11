using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logicScript : MonoBehaviour
{
    public int playerScore;
    public Text playerScoreText;
    public Text gameoverText;
    public Text highScoreText;
    public GameObject gameoverPanel;
    public static bool isGameOver;
    public int highScore;
    public stalagSpawner spawner;
    private const string HIGH_SCORE_KEY = "HighScore";

    private void Start()
    {
        // Load the saved high score ONCE on start using consistent key "HighScore"
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        
        if (AudioManager.instance != null && AudioManager.instance.bgmSound != null)
        {
            AudioManager.instance.PlayBGM(AudioManager.instance.bgmSound);
        }
    }

    void Awake()
    {
        // Reset static flags as soon as the scene loads
        isGameOver = false; 
       // PlayerPrefs.DeleteAll();
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
        if (isGameOver) return;
    
        isGameOver = true;

        if (spawner != null)
        {
            spawner.isSpawning = false;
        }
    
        if (AudioManager.instance != null && AudioManager.instance.musicSource != null)
        {
            AudioManager.instance.musicSource.Stop();
        }
    
        Debug.Log($"[GameOver Check] Player Score: {playerScore} | Stored High Score: {highScore}");
    
        if (playerScore > highScore)
        {
            // Save new high score
            highScore = playerScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, playerScore);
            PlayerPrefs.Save();

            // Display centered header + high score subtext in one text component
            if (gameoverText != null) 
            {
                gameoverText.gameObject.SetActive(true);
                gameoverText.verticalOverflow = VerticalWrapMode.Overflow;
                gameoverText.horizontalOverflow = HorizontalWrapMode.Overflow;
                gameoverText.alignment = TextAnchor.MiddleCenter;
                gameoverText.text = "Congratulations!\nHigh Score: " + highScore + "!";
            }

            if (AudioManager.instance != null)
            {
                AudioManager.instance.playSFX(AudioManager.instance.highScoreSound);
            }
        }
        else
        {
            // Display centered game over header + best score subtext in one text component
            if (gameoverText != null) 
            {
                gameoverText.gameObject.SetActive(true);
                gameoverText.verticalOverflow = VerticalWrapMode.Overflow;
                gameoverText.horizontalOverflow = HorizontalWrapMode.Overflow;
                gameoverText.alignment = TextAnchor.MiddleCenter;
                gameoverText.text = "Game Over\nBest Score: " + highScore;
            }

            if (AudioManager.instance != null)
            {
                AudioManager.instance.playSFX(AudioManager.instance.gameoverSound);
            }
        }

        gameoverPanel.SetActive(true);
    }
}
