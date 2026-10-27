using System.Collections;
using UnityEngine;

public class CursorShaker : MonoBehaviour
{
    public float shakeSpeed = 5f;
    public float shakeIntensity = 250f;
    private bool shaking = false;

    public void StartShaking()
    {
        if (!shaking)
        {
            shaking = true;
            StartCoroutine(ShakeHand());
        }
    }

    public void StopShaking()
    {
        if (shaking)
        {
            shaking = false;
            StopCoroutine(ShakeHand());
            CustomCursor.instance.ResetOffset();
        }
    }
    private IEnumerator ShakeHand()
    {
        while (shaking)
        {
            float time = Time.time * shakeSpeed;
            float x = (Mathf.PerlinNoise(0f , time) * 2f - 1f) * shakeIntensity;
            float y = (Mathf.PerlinNoise(500f , -time) * 2f - 1f) * shakeIntensity;
            yield return null;

            CustomCursor.instance.SetOffset(new Vector2(x, y));
        }
    }
}
