using UnityEngine;

public class ScrollSwap : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] GameObject openScroll;
    [SerializeField] GameObject closedScroll;
    [SerializeField] bool isOpen = false;

    public void OnClick()
    {
        ToggleScroll();
    }

    public void OnHold()
    {
        
    }

    public void OnRelease()
    {
        
    }

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