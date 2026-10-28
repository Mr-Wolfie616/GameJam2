using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] splatterPrefabs;
    [SerializeField] private RectTransform splatterContainer;
    [Header("Configurables")]
    [SerializeField] private int maxSplatters = 30;

    public void IncreaseSplatter()
    {
        if (splatterPrefabs == null || splatterPrefabs.Length == 0 || splatterContainer == null)
        {
            Debug.LogWarning("Please assign splatter prefabs and the splatter container in the inspector!");
            return;
        }

        if (splatterContainer.childCount >= maxSplatters)
        {
            Destroy(splatterContainer.GetChild(0).gameObject);
        }

        int randomIndex = Random.Range(0, splatterPrefabs.Length);
        GameObject chosenPrefab = splatterPrefabs[randomIndex];

        GameObject newSplatter = Instantiate(chosenPrefab, splatterContainer);

        RectTransform rectTransform = newSplatter.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            float containerWidth = splatterContainer.rect.width;
            float containerHeight = splatterContainer.rect.height;

            float randomX = Random.Range(-containerWidth / 2f, containerWidth / 2f);
            float randomY = Random.Range(-containerHeight / 2f, containerHeight / 2f);

            rectTransform.anchoredPosition = new Vector2(randomX, randomY);

            rectTransform.localRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            float randomScale = Random.Range(0.7f, 1.3f);
            rectTransform.localScale = new Vector3(randomScale, randomScale, 1f);
        }
    }
}