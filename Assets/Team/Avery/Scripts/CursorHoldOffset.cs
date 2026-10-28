using UnityEngine;

public class CursorHoldOffset : MonoBehaviour
{
    public Vector3 offset = new();

    public Vector3 GetCursorWorldPosition()
    {
        Vector3 mousePosition = CustomCursor.instance.GetCursorScreenPos();
        mousePosition += offset;

        if (mousePosition.z == 0)
        {
            mousePosition.z = Camera.main.nearClipPlane;
        }

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        return worldPosition;
    }
}
