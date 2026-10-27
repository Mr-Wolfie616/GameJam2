using UnityEngine;

// changes the employee of the month poster each time an order is failed
// reads FailedOrders from Jack's LoseConCheck script
public class EmployeePoster : MonoBehaviour
{
    // 5 lives - each failed order moves the poster one state closer to death
    // FiveFails is the last poster, shown on the 5th fail when Jack's script triggers death
    public enum PosterState
    {
        Fresh,
        OneFail,
        TwoFails,
        ThreeFails,
        FourFails,
        FiveFails
    }

    public PosterState currentState = PosterState.Fresh;

    [Header("References")]
    public LoseConCheck loseConCheck;       // finds it in the scene if left empty

    // one poster object per state, in the same order as the enum (fresh first)
    // only the one for the current state is switched on
    public GameObject[] posterObjects = new GameObject[6];

    private int lastFailedOrders = -1;       // so the poster updates on the first frame

    void Start()
    {
        if (loseConCheck == null)
        {
            loseConCheck = FindFirstObjectByType<LoseConCheck>();
        }

        if (loseConCheck == null)
        {
            Debug.LogError("EmployeePoster couldnt find a LoseConCheck in the scene");
        }

        SetState(PosterState.Fresh);
    }

    void Update()
    {
        if (loseConCheck == null)
        {
            return;
        }

        // only change the poster when the number of fails actually changes
        if (loseConCheck.FailedOrders != lastFailedOrders)
        {
            lastFailedOrders = loseConCheck.FailedOrders;

            // stays on the last poster once you're past it (death is handled in Jack's script)
            int stateIndex = Mathf.Clamp(lastFailedOrders, 0, (int)PosterState.FiveFails);
            SetState((PosterState)stateIndex);
        }
    }

    public void SetState(PosterState newState)
    {
        currentState = newState;

        int index = (int)currentState;

        // turn on the poster for this state and turn the rest off
        for (int i = 0; i < posterObjects.Length; i++)
        {
            if (posterObjects[i] != null)
            {
                posterObjects[i].SetActive(i == index);
            }
        }

        if (index >= posterObjects.Length || posterObjects[index] == null)
        {
            Debug.LogWarning("Missing poster object for state " + currentState);
        }
    }

    // test only - right click the component header to fake a failed order
    [ContextMenu("Test: Fail Order")]
    private void TestFailOrder()
    {
        if (loseConCheck != null)
        {
            loseConCheck.FailedOrders++;
        }
        else if (currentState != PosterState.FiveFails)
        {
            SetState(currentState + 1);
        }
    }
}
