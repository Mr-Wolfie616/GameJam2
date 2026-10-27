using UnityEngine;

public class ScrollSwap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject openScroll;
    [SerializeField] GameObject closedScroll;
    [SerializeField] bool isOpen = false;

    public void ToggleScroll()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            openScroll.SetActive(true);
            closedScroll.SetActive(false);
        }
        else
        {
            openScroll.SetActive(false);
            closedScroll.SetActive(true);
        }
    }
}