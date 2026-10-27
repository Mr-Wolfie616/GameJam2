using UnityEngine;
using UnityEngine.Events;

public class LoseConCheck : MonoBehaviour
{
    public int SuccessfulOrders = 0;
    public int FailedOrders = 0;
    private int AmountOfFailedOrdersBeforeYouLoseTheGame = 5;
    private bool DidThePlayerManageToCompleteEverythingCorrectly = false;

    void OnEnable()
    {
        JarHandler.OnSucceed += PlayerDidIt;
        JarHandler.OnFail += PlayerDidntIt;
        Timer.TimerEnd += TimerEnds;
    }

    void PlayerDidIt() { DidThePlayerManageToCompleteEverythingCorrectly = true; }
    void PlayerDidntIt() { DidThePlayerManageToCompleteEverythingCorrectly = true; }
    void TimerEnds()
    {
        if(DidThePlayerManageToCompleteEverythingCorrectly) SuccessfulOrders++;
        else FailedOrders++;
        if(FailedOrders >= AmountOfFailedOrdersBeforeYouLoseTheGame) YouLose();
    }

    void YouLose()
    {
        SuccessfulOrders = 0;
        FailedOrders = 0;
    }
}
