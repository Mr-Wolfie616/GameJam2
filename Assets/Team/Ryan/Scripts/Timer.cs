using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    // the 5 milestones of the timer, each one has its own looping sprites
    public enum TimerState
    {
        Full,           // 100% - 75%
        ThreeQuarters,  // 75% - 50%
        Half,           // 50% - 25%
        Quarter,        // 25% - 0%
        Empty           // 0%
    }

    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 60f;   // this is also the time it resets back to

    [Header("Animation")]
    // use whichever one the timer is (Image for UI, SpriteRenderer for in the world), leave the other empty
    [SerializeField] Image timerImage;
    [SerializeField] SpriteRenderer timerRenderer;
    [SerializeField] float framesPerSecond = 8f;

    // looping sprites for each milestone (just put 1 sprite in if it doesnt animate)
    [SerializeField] Sprite[] fullSprites;
    [SerializeField] Sprite[] threeQuarterSprites;
    [SerializeField] Sprite[] halfSprites;
    [SerializeField] Sprite[] quarterSprites;
    [SerializeField] Sprite[] emptySprites;

    // plays once when the timer is refreshed, then the timer goes back to full
    [SerializeField] Sprite[] refreshSprites;

    // events the game manager can listen to
    public static event Action TimerEnd;                        // time ran out (or CancelTimer was called)
    public static event Action<TimerState> TimerStateChanged;   // hit a new milestone
    public static event Action TimerRefreshStarted;             // refresh animation started
    public static event Action TimerRefreshFinished;            // refresh animation done and timer is full again

    private float startTime;
    private TimerState currentState = TimerState.Full;
    private bool timerEnded = false;
    private bool refreshing = false;

    private Sprite[] currentSprites;
    private int currentFrame = 0;
    private float frameTimer = 0f;

    void Awake()
    {
        // remember the starting time so the timer can reset back to it
        startTime = remainingTime;
    }

    void Start()
    {
        SetState(TimerState.Full);
        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        // the timer doesnt count down while the refresh animation is playing
        if (refreshing)
        {
            PlayRefreshAnimation();
            return;
        }

        //  if remainingTime is bigger then 0 carry on
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            // when it hits 0 stop the timer and tell anything listening
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                EndTimer("Game Over");
            }
        }

        // change to the next milestone if we've passed one
        TimerState newState = GetStateFromTime();
        if (newState != currentState)
        {
            SetState(newState);
        }

        UpdateTimerText();
        PlayLoopingAnimation();
    }

    public void CancelTimer()
    {
        remainingTime = 0;
        EndTimer("Game Ended Early");
    }

    // the game manager calls this when the next body comes in
    // plays the refresh animation then resets the timer to full
    public void RefreshTimer()
    {
        if (refreshing)
        {
            return;
        }

        refreshing = true;
        currentFrame = 0;
        frameTimer = 0f;

        if (TimerRefreshStarted != null)
        {
            TimerRefreshStarted();
        }

        // no refresh sprites so just reset straight away
        if (refreshSprites == null || refreshSprites.Length == 0)
        {
            FinishRefresh();
            return;
        }

        SetSprite(refreshSprites[0]);
    }

    // ---- things the game manager can check ----

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public float GetStartTime()
    {
        return startTime;
    }

    // returns 0 to 100
    public float GetPercentLeft()
    {
        return remainingTime / startTime * 100f;
    }

    public TimerState GetCurrentState()
    {
        return currentState;
    }

    public bool IsRefreshing()
    {
        return refreshing;
    }

    public bool HasEnded()
    {
        return timerEnded;
    }

    // private functions 

    private void EndTimer(string message)
    {
        if (timerEnded)
        {
            return;
        }

        timerEnded = true;

        if (TimerEnd != null)
        {
            TimerEnd();
        }

        Debug.Log(message);
    }

    // works out which milestone the timer should be on
    private TimerState GetStateFromTime()
    {
        float percent = GetPercentLeft();

        if (percent > 75f) return TimerState.Full;
        if (percent > 50f) return TimerState.ThreeQuarters;
        if (percent > 25f) return TimerState.Half;
        if (percent > 0f) return TimerState.Quarter;
        return TimerState.Empty;
    }

    private void SetState(TimerState newState)
    {
        currentState = newState;
        currentSprites = GetSpritesForState(newState);

        // start the new animation from the first frame
        currentFrame = 0;
        frameTimer = 0f;

        if (currentSprites != null && currentSprites.Length > 0)
        {
            SetSprite(currentSprites[0]);
        }

        if (TimerStateChanged != null)
        {
            TimerStateChanged(newState);
        }
    }

    private Sprite[] GetSpritesForState(TimerState state)
    {
        switch (state)
        {
            case TimerState.Full: return fullSprites;
            case TimerState.ThreeQuarters: return threeQuarterSprites;
            case TimerState.Half: return halfSprites;
            case TimerState.Quarter: return quarterSprites;
            default: return emptySprites;
        }
    }

    // loops the sprites for the current milestone
    private void PlayLoopingAnimation()
    {
        if (currentSprites == null || currentSprites.Length == 0)
        {
            return;
        }

        frameTimer += Time.deltaTime;

        if (frameTimer >= 1f / framesPerSecond)
        {
            frameTimer = 0f;
            currentFrame = (currentFrame + 1) % currentSprites.Length;
            SetSprite(currentSprites[currentFrame]);
        }
    }

    // plays the refresh sprites once then resets the timer
    private void PlayRefreshAnimation()
    {
        frameTimer += Time.deltaTime;

        if (frameTimer >= 1f / framesPerSecond)
        {
            frameTimer = 0f;
            currentFrame++;

            if (currentFrame >= refreshSprites.Length)
            {
                FinishRefresh();
            }
            else
            {
                SetSprite(refreshSprites[currentFrame]);
            }
        }
    }

    private void FinishRefresh()
    {
        refreshing = false;
        remainingTime = startTime;
        timerEnded = false;

        SetState(TimerState.Full);
        UpdateTimerText();

        if (TimerRefreshFinished != null)
        {
            TimerRefreshFinished();
        }

        Debug.Log("Timer refreshed");
    }

    private void SetSprite(Sprite sprite)
    {
        if (timerImage != null)
        {
            timerImage.sprite = sprite;
        }

        if (timerRenderer != null)
        {
            timerRenderer.sprite = sprite;
        }
    }

    // this piece of code creates the integers for minutes and seconds and
    // formats them into a timer
    private void UpdateTimerText()
    {
        if (timerText == null)
        {
            return;
        }

        int Minutes = Mathf.FloorToInt(remainingTime / 60);
        int Seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
    }

    //test only  right click the component header to test (3 dots)

    [ContextMenu("Test: Refresh Timer")]
    private void TestRefreshTimer()
    {
        RefreshTimer();
    }

    [ContextMenu("Test: Skip 25% Of Time")]
    private void TestSkipTime()
    {
        remainingTime = Mathf.Max(0.01f, remainingTime - startTime * 0.25f);
    }
}
