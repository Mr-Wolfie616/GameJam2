using UnityEngine;

public class SurgeryTools : MonoBehaviour
{
    public static SurgeryTools instance;
    private ToolDisplayHelper toolDisplayHelper;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        toolDisplayHelper = GetComponent<ToolDisplayHelper>();
    }

    public ToolType heldTool;

    public void SetHeldTool(ToolType tool)
    {
        heldTool = tool;

        if (tool == ToolType.None)
        {
            CustomCursor.instance.CursorPoint();
            return;
        }

        var sprite = toolDisplayHelper.GetSpriteForTool(tool);
        if (sprite != null)
        {
            CustomCursor.instance.SetCurrentCursor(sprite);
        }
    }
}