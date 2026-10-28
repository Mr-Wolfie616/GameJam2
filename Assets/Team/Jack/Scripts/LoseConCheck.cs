using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Events;

public class LoseConCheck : MonoBehaviour
{
    public int SuccessfulOrders = 0;
    public int FailedOrders = 0;
    private int AmountOfFailedOrdersBeforeYouLoseTheGame = 5;
    public bool DidThePlayerManageToCompleteEverythingCorrectly = false;

    void OnEnable()
    {
        JarHandler.OnSucceed += PlayerDidIt;
        JarHandler.OnFail += PlayerDidntIt;
        NewTimer.onTimerEnd += TimerEnds;
    }

    void PlayerDidIt() { DidThePlayerManageToCompleteEverythingCorrectly = true; }
    void PlayerDidntIt() { DidThePlayerManageToCompleteEverythingCorrectly = false; }
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
