using System;
using UnityEngine;
public class BloodSplatterEvent : RandomEvent
{
    public override void TriggerEvent()
    {
        base.TriggerEvent();
        Debug.Log("Blood Splatter Event Triggered");
    }

    public override void EndEvent()
    {
        base.EndEvent();
        Debug.Log("Blood Splatter Event Ended");
    }
}
