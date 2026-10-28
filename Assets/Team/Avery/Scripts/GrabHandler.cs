using UnityEngine;

public class GrabHandler : MonoBehaviour
{
    public static GrabHandler Instance { get; private set; }
    private CursorHoldOffset cursorHoldOffset;
    private Transform heldParent;
    private Transform holdPt;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        cursorHoldOffset = GetComponentInChildren<CursorHoldOffset>();
    }

    private void Update()
    {
        Vector3 pos = cursorHoldOffset.GetCursorWorldPosition();
        cursorHoldOffset.gameObject.transform.position = pos;
        if (holdPt != null)
        {
            holdPt.position = pos;
        }
    }

    public void AttachObject(GameObject obj, Transform holdPoint)
    {
        holdPt = holdPoint;
    }

    public void LetGo(GameObject obj, Transform holdPoint)
    {
        holdPt = null;
    }
}
