using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public static CustomCursor instance;

    public Image cursorImage;

    // Cursor textures need to be 64x but exported 4x at 256x for scale
    public Sprite point;
    public Sprite click;
    public Sprite hover;
    public Sprite grab;
    private Sprite currentCursor;

    public Vector2 offset;
    private Vector2 storeOffset;
    private Vector2 mousePos;

    private Vector2 currentShake;
    public void SetOffset(Vector2 newOffset)
    {
        currentShake = newOffset;
        offset = storeOffset + newOffset;
    }

    public void ResetOffset()
    {
        currentShake = Vector2.zero;
        offset = storeOffset;
    }

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
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        CursorClick();
        storeOffset = offset;
    }

    public void SetCurrentCursor(Sprite cursor)
    {
        currentCursor = cursor;

        if (currentCursor == null)
        {
            currentCursor = point;
        }

        cursorImage.sprite = currentCursor;
        cursorImage.SetNativeSize();
    }

    private void Update()
    {
        mousePos = Input.mousePosition;
        Vector2 cursorPosition = mousePos + offset;
        cursorImage.transform.position = cursorPosition;
    }

    public Vector2 GetCursorScreenPos()
    {
        return mousePos + currentShake;
    }

    public Vector3 GetCursorWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(GetCursorScreenPos());
    }

    public void CursorClick() { SetCurrentCursor(click); }
    public void CursorPoint() { SetCurrentCursor(point); }
    public void CursorHover() { SetCurrentCursor(hover); }
    public void CursorGrab() { SetCurrentCursor(grab); }
}
