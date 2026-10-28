using UnityEngine;

public class ToolInteractable : MonoBehaviour, IInteractable
{
    public ToolType toolType;
    private SpriteRenderer spriteRenderer;
    public LayerMask a;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnClick()
    {
        SurgeryTools.instance.SetHeldTool(toolType);
        spriteRenderer.enabled = false;
    }

    public void OnHold()
    {
    }

    public void OnRelease()
    {
        spriteRenderer.enabled = true;
        SurgeryTools.instance.SetHeldTool(ToolType.None);

        CheckRibs()?.smashs();
    }

    public RibSmash CheckRibs()
    {
        Ray ray = Camera.main.ScreenPointToRay(CustomCursor.instance.GetCursorScreenPos());
        RibSmash thing = null;
        if (Physics.Raycast(ray, out RaycastHit hit3D, 50f, a))
        {
            Debug.Log("Knkjsfkjhkjfku");
            thing = hit3D.collider.gameObject.GetComponent<RibSmash>();
        }
        return thing;
    }
}
