using System;
using System.Collections;
using UnityEngine;
public class ParanoiaBloodletEvent : RandomEvent
{
    public override void TriggerEvent()
    {
        base.TriggerEvent();
        Debug.Log("Paranoia Event Triggered");

    }

    public override void EndEvent()
    {
        base.EndEvent();
        Debug.Log("Paranoia Event Ended");
    }
}
