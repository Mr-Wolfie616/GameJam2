using UnityEngine;

public class ParanoiaBloodletEvent : RandomEvent
{
    BloodLettingEvent ParanoiaManagerScript;

    public override void TriggerEvent()
    {
        base.TriggerEvent();
        Debug.Log("Paranoia Event Triggered");

        ParanoiaManagerScript = GameObject.FindFirstObjectByType<BloodLettingEvent>();
        ParanoiaManagerScript.on = true;
    }

    public override void EndEvent()
    {
        base.EndEvent();
        Debug.Log("Paranoia Event Ended");

        ParanoiaManagerScript.leech = true;
    }
}
