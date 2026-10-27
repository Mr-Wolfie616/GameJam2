using System;
using UnityEngine;
public class ShakyHandEvent : RandomEvent
{
    public float shakeIntensity = 1.0f; 
    public float shakeSpeed = 1.0f;
    public override void TriggerEvent()
    {
        base.TriggerEvent();
        Debug.Log("Shaky Hand Event Triggered");
        // Add logic to make the player's hand shake
    }

    public override void EndEvent()
    {
        base.EndEvent();
        Debug.Log("Shaky Hand Event Ended");
        // Add logic to stop the player's hand from shaking
    }
}
