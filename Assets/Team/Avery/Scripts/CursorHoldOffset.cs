using UnityEngine;

public class CursorHoldOffset : MonoBehaviour
{
    public float offsetDistance = 3.0f;
    public Vector3 GetCursorWorldPosition()
    {
        Vector3 mousePosition = CustomCursor.instance.GetCursorScreenPos();
        mousePosition.z = offsetDistance;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        return worldPosition;
    }
}
