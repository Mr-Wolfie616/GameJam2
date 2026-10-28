using UnityEngine;

public class ToolInteractable : MonoBehaviour, IInteractable
{
    public ToolType toolType;
    private SpriteRenderer spriteRenderer;

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
    }
}
