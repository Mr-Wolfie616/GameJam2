using System;
using UnityEngine;

public class RandomEvent
{
    public virtual void TriggerEvent()
    {
        Debug.Log("Triggered Random Event");
    }
    public virtual void EndEvent()
    {
        Debug.Log("Ended Random Event");
    }
}
