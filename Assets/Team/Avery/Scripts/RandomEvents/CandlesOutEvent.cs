using System;
using UnityEngine;
public class CandlesOutEvent : RandomEvent
{
    private Candle[] candles;
    private bool eventActive = false;

    public override void TriggerEvent()
    {
        base.TriggerEvent();
        Debug.Log("Candles Out Event Triggered");

        candles = GameObject.FindObjectsByType<Candle>(FindObjectsSortMode.None);
        eventActive = true;

        if (candles.Length == 0)
        {
            Debug.LogWarning("No candles in the scene, ending the candle event");
            EndEvent();
            return;
        }

        // blow out every candle and listen for when they get relit
        foreach (Candle candle in candles)
        {
            candle.onRelit += CheckCandles;
            candle.Extinguish();
        }
    }

    // called every time a candle is relit, ends the event once they're all lit
    private void CheckCandles()
    {
        foreach (Candle candle in candles)
        {
            if (!candle.IsLit())
            {
                return;
            }
        }

        EndEvent();
    }

    public override void EndEvent()
    {
        if (!eventActive)
        {
            return;
        }
        eventActive = false;

        foreach (Candle candle in candles)
        {
            candle.onRelit -= CheckCandles;

            // relights any candles still out if the event was ended early (like the E test key)
            if (!candle.IsLit())
            {
                candle.Relight();
            }
        }

        base.EndEvent();
        Debug.Log("Candles Out Event Ended");
    }
}