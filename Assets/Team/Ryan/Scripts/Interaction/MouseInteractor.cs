using UnityEngine;
using UnityEngine.EventSystems;

// put this on one object in the scene (the camera works fine)
// it fires a ray from the mouse and tells whatever IInteractable it hits that it was clicked, held or let go
public class MouseInteractor : MonoBehaviour
{
    public Camera cam;                          // uses the main camera if left empty
    public float maxDistance = 100f;
    public LayerMask interactLayers = ~0;       // everything by default

    private IInteractable heldObject;           
    private GameObject heldGameObject;          // used to check if the held object got destroyed

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam == null)
        {
            Debug.LogError("MouseInteractor requires a camera or a MainCamera-tagged camera.", this);
        }
    }

    void Update()
    {
        if (cam == null)
        {
            return;
        }

        Vector2 cursorScreenPos = CustomCursor.instance != null
            ? CustomCursor.instance.GetCursorScreenPos()
            : Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(cursorScreenPos);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
        IInteractable target = GetInteractableUnderMouse(ray);

        // mouse pressed this frame
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(
                target != null
                    ? $"Interaction ray fired and found {((MonoBehaviour)target).name}."
                    : "Interaction ray fired; no interactable was hit.",
                this);

            // dont click through UI buttons
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (target != null)
            {
                heldObject = target;
                heldGameObject = ((MonoBehaviour)target).gameObject;
                heldObject.OnClick();
            }
        }

        if (heldObject == null)
        {
            return;
        }

        // stop if the object we were holding got destroyed
        if (heldGameObject == null)
        {
            heldObject = null;
            return;
        }

        // mouse still held down
        if (Input.GetMouseButton(0))
        {
            heldObject.OnHold();
        }

        // mouse let go this frame
        if (Input.GetMouseButtonUp(0))
        {
            heldObject.OnRelease();
            heldObject = null;
            heldGameObject = null;
        }
    }

    // fires a ray from the camera through the mouse and returns the IInteractable it hits (or null)
    // checks both 3D colliders (organs in the body) and 2D colliders (tools etc) and picks whichever is closer
    private IInteractable GetInteractableUnderMouse(Ray ray)
    {
        Collider closestCollider3D = null;
        Collider2D closestCollider2D = null;
        float distance3D = Mathf.Infinity;
        float distance2D = Mathf.Infinity;

        // 3D colliders
        if (Physics.Raycast(ray, out RaycastHit hit3D, maxDistance, interactLayers))
        {
            closestCollider3D = hit3D.collider;
            distance3D = hit3D.distance;
        }

        // 2D colliders (a normal 3D raycast cant hit these so they need their own check)
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, maxDistance, interactLayers);
        if (hit2D.collider != null)
        {
            closestCollider2D = hit2D.collider;
            distance2D = hit2D.fraction * maxDistance;
        }

        // checks the parent too so the collider can be on a child object
        if (closestCollider2D != null && distance2D < distance3D)
        {
            return closestCollider2D.GetComponentInParent<IInteractable>();
        }

        if (closestCollider3D != null)
        {
            return closestCollider3D.GetComponentInParent<IInteractable>();
        }

        return null;
    }
}
