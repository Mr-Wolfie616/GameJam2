using UnityEngine;
using UnityEngine.Events;

public class LoseConCheck : MonoBehaviour
{
    private int SuccessfulOrders = 0;
    private int FailedOrders = 0;
    private int AmountOfFailedOrdersBeforeYouLoseTheGame = 5;

    public static UnityEvent PlayerSucceeds;
    public static UnityEvent PlayerFails;

    void OnEnable()
    {
        PlayerSucceeds.AddListener(PlayerDidIt);
        PlayerFails.AddListener(PlayerDidntIt);
    }

    void Update()
    {
        
    }

    void PlayerDidIt() { SuccessfulOrders++; }
    void PlayerDidntIt() 
    { 
        FailedOrders++; 
        if(FailedOrders >= AmountOfFailedOrdersBeforeYouLoseTheGame) YouLose();
    }

    void YouLose()
    {
        Debug.Log("You Lose");
    }
}
