using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject body;
    private GameObject currentBody;
    public float curtainCloseTime = 3f;
    public static GameManager Instance { get; private set; }
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
        NewBody();
    }

    private void OnEnable()
    {
        Timer.TimerEnd += NewBody;
    }

    public void NewBody()
    {
        StartCoroutine(CloseAndSpawn(curtainCloseTime));
    }

    private IEnumerator CloseAndSpawn(float time)
    {
        CurtainManager.Instance.CloseCurtains();

        yield return new WaitForSeconds(time/2f);

        if (currentBody != null)
        {
            Destroy(currentBody);
        }

        body.SetActive(false);
        currentBody = Instantiate(body, body.transform.position, body.transform.rotation);
        currentBody.SetActive(true);

        yield return new WaitForSeconds(time / 2f);

        CurtainManager.Instance.OpenCurtains();
    }
}
