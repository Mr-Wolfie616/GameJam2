using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    // Cursor textures need to be 64x but exported 4x at 256x for scale
    public Texture2D point;
    public Texture2D click;
    public Texture2D hover;
    public Texture2D grab;
    private Texture2D currentCursor;

    public Vector2 hotspot = new Vector2(56, 12);
    public CursorMode cursorMode;

    private void Start()
    {
        CursorClick();
    }

    public void SetCurrentCursor(Texture2D cursor)
    {
        currentCursor = cursor;

        if (currentCursor == null)
        {
            currentCursor = point;
        }

        Cursor.SetCursor(currentCursor, hotspot, cursorMode);
    }

    public void CursorClick() { SetCurrentCursor(click); }
    public void CursorPoint() { SetCurrentCursor(point); }
    public void CursorHover() { SetCurrentCursor(hover); }
    public void CursorGrab() { SetCurrentCursor(grab); }
}
