using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Splatter : MonoBehaviour
{
    private Image splatterImage;
    float stayDuration = 5f;

    private void Awake()
    {
        splatterImage = GetComponent<Image>();
    }

    private void Start()
    {
        float fadeDuration = Random.Range(5f, 30f);

        StartCoroutine(FadeOutRoutine(fadeDuration));
    }

    private IEnumerator FadeOutRoutine(float duration)
    {
        yield return new WaitForSeconds(stayDuration);

        float elapsedTime = 0f;
        Color startColor = splatterImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / duration);

            splatterImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        splatterImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        Destroy(gameObject);
    }
}