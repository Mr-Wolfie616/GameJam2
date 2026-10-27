using System;
using System.Collections;
using UnityEngine;
public class ShakyHandEvent : RandomEvent
{
    public bool shaking = false;

    private CursorShaker cursorShaker; //evil bc Unity doesn't allow coroutines in non-MonoBehaviours

    public override void TriggerEvent()
    {
        base.TriggerEvent();
        Debug.Log("Shaky Hand Event Triggered");
        shaking = true;

        GameObject cursorShakerObject = new GameObject("cursorShaker");
        cursorShaker = cursorShakerObject.AddComponent<CursorShaker>();
        cursorShaker.StartShaking();
    }

    public override void EndEvent()
    {
        base.EndEvent();
        Debug.Log("Shaky Hand Event Ended");
        shaking = false;

        if (cursorShaker != null)
        {
            cursorShaker.StopShaking();
            GameObject.Destroy(cursorShaker.gameObject);
            cursorShaker = null;
        }
    }
}
