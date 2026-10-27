using System.Collections;
using UnityEngine;

public class CursorShaker : MonoBehaviour
{
    public float minShakeSpeed = 0.2f;
    public float maxShakeSpeed = 1f;
    public float minIntensity = 50f;
    public float maxIntensity = 200f;
    private bool shaking = false;
    public float timeToMaxSpeed = 10f;
    public float jitterMulti = 0.025f;
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
        float elapsed = 0f;
        float duration = timeToMaxSpeed;
        while (shaking)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            float shakeSpeed = Mathf.Lerp(minShakeSpeed, maxShakeSpeed, t);
            float shakeIntensity = Mathf.Lerp(minIntensity, maxIntensity, t);

            float time = Time.time * shakeSpeed;
            float x = (Mathf.PerlinNoise(0f, time) * 2f - 1f) * shakeIntensity;
            float y = (Mathf.PerlinNoise(500f, -time) * 2f - 1f) * shakeIntensity;

            float xJitter = Random.Range(-shakeIntensity, shakeIntensity) * jitterMulti;
            float yJitter = Random.Range(-shakeIntensity, shakeIntensity) * jitterMulti;

            CustomCursor.instance.SetOffset(new Vector2(x + xJitter, y + yJitter));

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
