using UnityEngine;

public class RibSmash : MonoBehaviour, IInteractable
{
    public void OnClick()
    {
        
    }

    public void OnHold()
    {
    }

    public void OnRelease()
    {
        if (SurgeryTools.instance.heldTool != ToolType.Bonesaw)
        {
            return;
        }
        Destroy(gameObject);
    }
}
