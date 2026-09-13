using System.Collections;
using UnityEngine;

public class screen_shake : MonoBehaviour
{
    public float duration = 0.5f;
    public AnimationCurve curve;

    // THIS DEFINES TriggerShake SO logicScript CAN CALL IT
    public void TriggerShake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = transform.position;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float strength = curve.Evaluate(currentTime / duration);
            
            // Shakes relative to original position
            transform.position = originalPos + (Random.insideUnitSphere * strength);
            
            yield return null; // Waits for next frame safely
        }

        transform.position = originalPos; // Snaps back when done
    }
}