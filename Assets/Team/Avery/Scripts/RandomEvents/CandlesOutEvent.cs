using System;
using UnityEngine;
public class CandlesOutEvent : RandomEvent
{
    public override void TriggerEvent()
    {
        base.TriggerEvent();
        Debug.Log("Candles Out Event Triggered");
    }

    public override void EndEvent()
    {
        base.EndEvent();
        Debug.Log("Candles Out Event Ended");
    }
}
