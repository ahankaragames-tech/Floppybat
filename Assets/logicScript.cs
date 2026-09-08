using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logicScript : MonoBehaviour
{
    public int playerScore;
    public Text playerScoreText;
    public GameObject gameoverPanel;
    public static bool isGameOver;

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
        gameoverPanel.SetActive(true);
    }
}
