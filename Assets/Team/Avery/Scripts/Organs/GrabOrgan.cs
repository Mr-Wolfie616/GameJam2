using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class GrabOrgan : MonoBehaviour, IInteractable
{
    private bool overJar = false;
    private Organ organ;
    private OrganJar currentJar;
    private CursorHoldOffset cursorHoldOffset;
    private bool holding = false;
    private void Awake()
    {
        organ = GetComponent<Organ>();
        cursorHoldOffset = GetComponent<CursorHoldOffset>();
    }

    private void Update()
    {
        if (holding)
        {
            Vector3 pos = cursorHoldOffset.GetCursorWorldPosition();
            transform.position = pos;
        }
    }
    public void OnClick()
    {
        Debug.Log("Clicked on organ: " + organ.type.ToString());
        if (organ.inHand) { return; }

        organ.inHand = true;
        holding = true;
    }

    public void OnHold() 
    {
        
    }

    public void OnRelease()
    {
        if (!organ.inHand) { return; }
        
        organ.inHand = false;
        holding = false;

        if (overJar)
        {
            currentJar.FillJar(organ);
            {
                Debug.Log(Equals(currentJar.wantedOrgan, organ.type));
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OrganJar"))
        {
            currentJar = other.GetComponent<OrganJar>();
            if (currentJar != null)
            {
                overJar = true;
            }
        }
    }
}
