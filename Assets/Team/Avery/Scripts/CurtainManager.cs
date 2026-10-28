using UnityEngine;

public class CurtainManager : MonoBehaviour
{
    public static CurtainManager Instance { get; private set; }
    public CurtainMove curtainMove;
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
        if (curtainMove == null)
        {
            curtainMove = FindFirstObjectByType<CurtainMove>();
        }
    }

    public void OpenCurtains()
    {
        if (curtainMove != null)
        {
            curtainMove.OpenCurtains();
        }
    }

    public void CloseCurtains()
    {
        if (curtainMove != null)
        {
            curtainMove.CloseCurtains();
        }
    }
}
