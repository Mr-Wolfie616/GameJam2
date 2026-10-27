using System;
using UnityEngine;

public class RandomEvent
{
    public virtual void TriggerEvent()
    {
        RandomEventHandler.instance.currentEvent = this;
        RandomEventHandler.instance.StopEventTriggering();
        Debug.Log("Triggered Random Event");
    }
    public virtual void EndEvent()
    {
        RandomEventHandler.instance.currentEvent = null;
        RandomEventHandler.instance.StartEventTriggering();
        Debug.Log("Ended Random Event");
    }
}
