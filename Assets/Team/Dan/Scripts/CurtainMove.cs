using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CurtainMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform leftCurtain;
    [SerializeField] private RectTransform rightCurtain;

    [Header("Animation Settings")]
    [SerializeField] private float duration = 0.5f;

    [Header("Left Curtain Anchors")]
    [SerializeField] private Vector2 leftClosedMin = new Vector2(0f, 0f);
    [SerializeField] private Vector2 leftClosedMax = new Vector2(0.5f, 1f);
    [SerializeField] private Vector2 leftOpenMin = new Vector2(0f, 0f);
    [SerializeField] private Vector2 leftOpenMax = new Vector2(0f, 1f);

    [Header("Right Curtain Anchors")]
    [SerializeField] private Vector2 rightClosedMin = new Vector2(0.5f, 0f);
    [SerializeField] private Vector2 rightClosedMax = new Vector2(1f, 1f);
    [SerializeField] private Vector2 rightOpenMin = new Vector2(1f, 0f);
    [SerializeField] private Vector2 rightOpenMax = new Vector2(1f, 1f);

    private Coroutine currentAnimation;
    public void OpenCurtains()
    {
        if (currentAnimation != null) StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(AnimateCurtains(leftOpenMin, leftOpenMax, rightOpenMin, rightOpenMax));
    }
    public void CloseCurtains()
    {
        if (currentAnimation != null) StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(AnimateCurtains(leftClosedMin, leftClosedMax, rightClosedMin, rightClosedMax));
    }

    private IEnumerator AnimateCurtains(Vector2 targetLeftMin, Vector2 targetLeftMax, Vector2 targetRightMin, Vector2 targetRightMax)
    {
        float elapsedTime = 0f;

        Vector2 startLeftMin = leftCurtain.anchorMin;
        Vector2 startLeftMax = leftCurtain.anchorMax;
        Vector2 startRightMin = rightCurtain.anchorMin;
        Vector2 startRightMax = rightCurtain.anchorMax;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            t = Mathf.SmoothStep(0f, 1f, t);

            leftCurtain.anchorMin = Vector2.Lerp(startLeftMin, targetLeftMin, t);
            leftCurtain.anchorMax = Vector2.Lerp(startLeftMax, targetLeftMax, t);

            rightCurtain.anchorMin = Vector2.Lerp(startRightMin, targetRightMin, t);
            rightCurtain.anchorMax = Vector2.Lerp(startRightMax, targetRightMax, t);

            yield return null;
        }

        leftCurtain.anchorMin = targetLeftMin;
        leftCurtain.anchorMax = targetLeftMax;
        rightCurtain.anchorMin = targetRightMin;
        rightCurtain.anchorMax = targetRightMax;
    }
}