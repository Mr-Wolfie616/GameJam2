using UnityEngine;

// changes the employee of the month poster each time an order is failed
// reads FailedOrders from Jack's LoseConCheck script
public class EmployeePoster : MonoBehaviour
{
    // 5 lives - each failed order moves the poster one state closer to death
   
    public enum PosterState
    {
        Fresh,
        OneFail,
        TwoFails,
        ThreeFails,
        FourFails
    }

    public PosterState currentState = PosterState.Fresh;

    [Header("References")]
    public LoseConCheck loseConCheck;       // finds it in the scene if left empty
    public SpriteRenderer posterRenderer;   // uses the one on this object if left empty

    // one sprite per state, in the same order as the enum (fresh first)
    public Sprite[] posterSprites = new Sprite[5];

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

        if (posterRenderer == null)
        {
            posterRenderer = GetComponent<SpriteRenderer>();
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
            int stateIndex = Mathf.Clamp(lastFailedOrders, 0, (int)PosterState.FourFails);
            SetState((PosterState)stateIndex);
        }
    }

    public void SetState(PosterState newState)
    {
        currentState = newState;

        int index = (int)currentState;

        if (posterRenderer != null && index < posterSprites.Length && posterSprites[index] != null)
        {
            posterRenderer.sprite = posterSprites[index];
        }
        else
        {
            Debug.LogWarning("Missing poster sprite or renderer for state " + currentState);
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
        else if (currentState != PosterState.FourFails)
        {
            SetState(currentState + 1);
        }
    }
}
