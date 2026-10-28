using UnityEngine;

public class RibSmash : MonoBehaviour, IInteractable
{
    public void OnClick()
    {
        if (SurgeryTools.instance.heldTool != ToolType.Bonesaw)
        {
            return;
        }
        Destroy(gameObject);
    }

    public void OnHold()
    {
    }

    public void OnRelease()
    {
    }
}
