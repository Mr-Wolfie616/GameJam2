using UnityEngine;

public class AbandonChildren : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            child.SetParent(null);
        }
    }
}
