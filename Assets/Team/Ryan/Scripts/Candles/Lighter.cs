using UnityEngine;

// put this on the lighter (it also needs a collider so the MouseInteractor can click it)
// click and drag the lighter over a candle thats out to relight it, let go and it goes back to where it was
public class Lighter : MonoBehaviour, IInteractable
{
    public Camera cam;               
    public float lightRange = 60f;      // how close (in pixels on screen) the lighter has to be to a candle

    [Header("Sprites")]
    public SpriteRenderer lighterRenderer;      // uses the one on this object if left empty
    public Sprite[] idleSprites = new Sprite[4];
    public float framesPerSecond = 8f;

    private Candle[] candles;
    private Vector3 startPosition;
    private float distanceFromCamera;

    private int currentFrame = 0;
    private float frameTimer = 0f;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (lighterRenderer == null)
        {
            lighterRenderer = GetComponent<SpriteRenderer>();
        }

        candles = FindObjectsByType<Candle>(FindObjectsSortMode.None);
    }

    // loops the idle sprites
    void Update()
    {
        if (lighterRenderer == null || idleSprites.Length == 0)
        {
            return;
        }

        frameTimer += Time.deltaTime;

        if (frameTimer >= 1f / framesPerSecond)
        {
            frameTimer = 0f;
            currentFrame = (currentFrame + 1) % idleSprites.Length;
            lighterRenderer.sprite = idleSprites[currentFrame];
        }
    }

    public void OnClick()
    {
        startPosition = transform.position;

        // remember how far away the lighter is so it stays at the same depth while dragging
        distanceFromCamera = cam.WorldToScreenPoint(transform.position).z;
    }

    public void OnHold()
    {
        // follow the mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distanceFromCamera;
        transform.position = cam.ScreenToWorldPoint(mousePosition);

        // relight any candle that's out and close enough
        foreach (Candle candle in candles)
        {
            if (candle.IsOut() && IsNear(candle))
            {
                candle.Relight();
            }
        }
    }

    public void OnRelease()
    {
        transform.position = startPosition;
    }

    // checks the distance on screen so it works whichever way the camera is facing
    private bool IsNear(Candle candle)
    {
        Vector2 lighterOnScreen = cam.WorldToScreenPoint(transform.position);
        Vector2 candleOnScreen = cam.WorldToScreenPoint(candle.transform.position);

        return Vector2.Distance(lighterOnScreen, candleOnScreen) <= lightRange;
    }
}
