using NUnit.Framework;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class RandomEventHandler : MonoBehaviour
{
    public static RandomEventHandler instance;
    public List<RandomEvent> randomEvents = new();
    public RandomEvent currentEvent;
    public float timeBetweenEvents;
    public bool canTriggerEvents = false;
    
    private BloodSplatterEvent bloodSplatterEvent;
    private ParanoiaBloodletEvent paranoiaBloodletEvent;
    private ShakyHandEvent shakyHandEvent;
    private CandlesOutEvent candlesOutEvent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bloodSplatterEvent = new BloodSplatterEvent();
        paranoiaBloodletEvent = new ParanoiaBloodletEvent();
        shakyHandEvent = new ShakyHandEvent();
        candlesOutEvent = new CandlesOutEvent();

        randomEvents.Add(bloodSplatterEvent);
        randomEvents.Add(paranoiaBloodletEvent);
        randomEvents.Add(shakyHandEvent);
        randomEvents.Add(candlesOutEvent);

        StartEventTriggering();
    }

    public void StartEventTriggering()
    {
        canTriggerEvents = true;
        StartCoroutine(WaitForTimeAndTrigger(timeBetweenEvents));
    }
    public void StopEventTriggering()
    {
        canTriggerEvents = false;
        StopCoroutine(WaitForTimeAndTrigger(timeBetweenEvents));
    }

    private IEnumerator WaitForTimeAndTrigger(float t)
    {
        while (canTriggerEvents)
        {
            Debug.Log("Triggering random event after waiting for " + t + " seconds.");
            yield return new WaitForSeconds(t);
            TriggerRandomEvent();
        }
    }

    public void TriggerRandomEvent()
    {
        if (currentEvent != null)
        {
            currentEvent.EndEvent();
            Debug.LogWarning("Force Ending Event");
            return;
        }

        if (randomEvents.Count == 0)
        {
            Debug.LogWarning("No random events assigned to the RandomEventHandler.");
            return;
        }
        int randomIndex = Random.Range(0, randomEvents.Count);
        RandomEvent selectedEvent = randomEvents[randomIndex];

        if (selectedEvent != null)
        {
            currentEvent = selectedEvent;
            selectedEvent.TriggerEvent();
            Debug.Log($"Triggered random event: {selectedEvent.GetType().Name} at index {randomIndex}.");
        }
        else
        {
            Debug.LogWarning($"Random event at index {randomIndex} is null.");
        }
    }

    private void Update()
    {
        // For Testing
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerRandomEvent();
        }
    }
}
