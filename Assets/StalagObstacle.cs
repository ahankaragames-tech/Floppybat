using System.Collections;
using UnityEngine;

public class StalagObstacle : MonoBehaviour
{
    public Transform stalactite;
    public Transform stalagmite;
    public float openSpeed = 8f;

    private float originalTopY;
    private float originalBottomY;
    private float closedMidpointY;
    private bool hasOpened = false;
    private Coroutine openCoroutine;

    void Awake()
    {
        // 1. Assign children by exact name (matches hierarchy spelling)
        if (stalactite == null) stalactite = transform.Find("Stalactite");
        if (stalagmite == null) stalagmite = transform.Find("Stalagmite");

        // Fallback to child order if names don't match
        if (stalactite == null && transform.childCount > 0) stalactite = transform.GetChild(0);
        if (stalagmite == null && transform.childCount > 1) stalagmite = transform.GetChild(1);

        // 2. Save original open positions from prefab default layout
        if (stalactite != null) originalTopY = stalactite.localPosition.y;
        if (stalagmite != null) originalBottomY = stalagmite.localPosition.y;

        // 3. Calculate exact tip-touch midpoint
        closedMidpointY = ((originalTopY + originalBottomY) / 2f);
    }

    void Start()
    {
        // If mutation is active, clamp both tips together at the exact midpoint on spawn
        if (logicScript.isMutationActive)
        {
            SetPositions(closedMidpointY, closedMidpointY);
        }
    }

    public void OpenGap()
    {
        // Guard against duplicate execution or running when mutation isn't active
        if (!hasOpened && logicScript.isMutationActive)
        {
            hasOpened = true; // Set flag immediately to prevent re-triggering
            if (openCoroutine != null) StopCoroutine(openCoroutine);
            openCoroutine = StartCoroutine(AnimateToOriginalPositions());
        }
    }

    private IEnumerator AnimateToOriginalPositions()
    {
        if (stalactite == null || stalagmite == null) yield break;

        while (Mathf.Abs(stalactite.localPosition.y - originalTopY) > 0.05f)
        {
            float newTopY = Mathf.MoveTowards(stalactite.localPosition.y, originalTopY, openSpeed * Time.deltaTime);
            float newBottomY = Mathf.MoveTowards(stalagmite.localPosition.y, originalBottomY, openSpeed * Time.deltaTime);

            SetPositions(newTopY, newBottomY);
            yield return null;
        }

        // Snap precisely to exact original positions at the end of animation
        SetPositions(originalTopY, originalBottomY);
    }

    private void SetPositions(float topY, float bottomY)
    {
        if (stalactite != null)
            stalactite.localPosition = new Vector3(stalactite.localPosition.x, topY, stalactite.localPosition.z);

        if (stalagmite != null)
            stalagmite.localPosition = new Vector3(stalagmite.localPosition.x, bottomY, stalagmite.localPosition.z);
    }
}