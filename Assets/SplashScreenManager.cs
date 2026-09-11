using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private CanvasGroup splashGroup;
    

    [Header("Timing Settings")]
    [SerializeField] private float fadeInDuration = 0.25f;   // Slow fade in
    [SerializeField] private float displayDuration = 0.75f;  // Stay on screen
    [SerializeField] private float fadeOutDuration = 0.25f;  // Slow fade out
    
    [Header("Next Scene")]
    [SerializeField] private string nextSceneName = "MainMenu"; // Exact name of your next scene

    private void Start()
    {
        if (splashGroup != null)
        {
            splashGroup.alpha = 0f; // Start hidden
            StartCoroutine(PlaySplashScreen());
        }
        else
        {
            Debug.LogError("SplashGroup CanvasGroup reference is missing!");
        }
    }

    private IEnumerator PlaySplashScreen()
    {
        // 1. Fade In
        float timer = 0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            splashGroup.alpha = Mathf.Clamp01(timer / fadeInDuration);
            yield return null;
        }
        splashGroup.alpha = 1f;

        // 2. Display Hold
        yield return new WaitForSeconds(displayDuration);

        // 3. Fade Out
        timer = 0f;
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            splashGroup.alpha = Mathf.Clamp01(1f - (timer / fadeOutDuration));
            yield return null;
        }
        splashGroup.alpha = 0f;

        // 4. Transition Scene
        SceneManager.LoadScene(nextSceneName);
    }
}