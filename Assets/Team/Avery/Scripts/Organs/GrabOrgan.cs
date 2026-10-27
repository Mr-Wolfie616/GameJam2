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

    public LayerMask jarLayer;
    private float maxDistance = 100f;
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
        //Debug.Log("Clicked on organ: " + organ.type.ToString());
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

        currentJar = CheckJar();

        if (currentJar != null)
        {
            overJar = true;
            currentJar.FillJar(organ);
            {
                Debug.Log(Equals(currentJar.wantedOrgan, organ.type));
            }
        }
        else
        {
            overJar = false;
        }
    }

    public OrganJar CheckJar()
    {
        Ray ray = Camera.main.ScreenPointToRay(CustomCursor.instance.GetCursorScreenPos());
        OrganJar jar = null;
        if (Physics.Raycast(ray, out RaycastHit hit3D, maxDistance, jarLayer))
        {
            jar = hit3D.collider.gameObject.GetComponent<OrganJar>();
        }
        return jar;
    }
}
