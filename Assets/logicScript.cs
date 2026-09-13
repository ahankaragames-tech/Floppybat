using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class logicScript : MonoBehaviour
{
    public int playerScore;
    public Text playerScoreText;
    public Text gameoverText;
    public Text highScoreText;
    public GameObject gameoverPanel;
    public static bool isGameOver;
    public int highScore;
    private const string HIGH_SCORE_KEY = "HighScore";

    // Mutation tracking
    public static bool isMutationActive;
    public int mutationThreshold = 10;
    
    void Awake()
    {
        // Reset static flags as soon as the scene loads
        isGameOver = false;
        isMutationActive = false;
        //PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        // Load the saved high score ONCE on start using consistent key "HighScore"
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        
        if (AudioManager.instance != null && AudioManager.instance.bgmSound != null)
        {
            AudioManager.instance.PlayBGM(AudioManager.instance.bgmSound);
        }
    }
    
    void Update()
    {
        // Check if player tapped/clicked, BUT ignore if tapping on UI buttons
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI())
            {
                return; // Exit early so jump/tap code doesn't block UI button touches!
            }
            // Put any global screen tap logic here if needed
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Optional: Pause audio or save score here
            Application.Quit();
        }
    }

    // Mobile-friendly UI raycast check for Android & PC Editor
    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;

        // Check finger touch ID on mobile devices
        if (Input.touchCount > 0)
        {
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }

        // Check mouse position on PC Editor / Standalone
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void addScore()
    {
        if (isGameOver) return;
        playerScore++;
        playerScoreText.text = playerScore.ToString();
        
        // 1. Check if we reached score 10
        if (playerScore >= mutationThreshold && !isMutationActive)
        {
            isMutationActive = true;
            Debug.Log("Mutation Triggered: Dynamic Openings Activated!");
        }

        // 2. If mutation is active, trigger the next obstacle to open
        if (isMutationActive)
        {
            NotifyNextObstacleToOpen();
        }
    }
    
    private void NotifyNextObstacleToOpen()
    {
        // Find all active stalagmites currently in the scene
        StalagObstacle[] activeObstacles = FindObjectsOfType<StalagObstacle>();
    
        // Loop through each one and call its OpenGap method
        foreach (StalagObstacle obstacle in activeObstacles)
        {
            obstacle.OpenGap();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        if (isGameOver) return;
    
        isGameOver = true;
     
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

        if (gameoverPanel != null)
        {
            gameoverPanel.SetActive(true);
        }
    }
}