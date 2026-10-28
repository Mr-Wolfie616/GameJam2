using UnityEngine;
using System;
using UnityEngine.Events;

public class NewTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float duration;
    [SerializeField] private bool startOnAwake = true;

    [Header("Animator & Triggers")]
    [SerializeField] private Animator animator;
    [SerializeField] private string resetAnimatorTrigger = "Reset";

    public static event Action onTimerStart;
    public static event Action onTimerEnd;

    [Header("Percentage Milestone Events")]
    public UnityEvent on100Percent;
    public UnityEvent on75Percent;
    public UnityEvent on50Percent;
    public UnityEvent on25Percent;
    public UnityEvent on0Percent;

    [Header("Animation Clips")]
    [SerializeField] private AnimationClip hundred;
    [SerializeField] private AnimationClip seventyFive;
    [SerializeField] private AnimationClip fifty;
    [SerializeField] private AnimationClip twentyFive;
    [SerializeField] private AnimationClip zero;
    [SerializeField] private AnimationClip reset;

    private float currentTime;
    private bool isRunning = false;

    private bool triggered100 = false;
    private bool triggered75 = false;
    private bool triggered50 = false;
    private bool triggered25 = false;
    private bool triggered0 = false;

    public float CurrentTime => currentTime;
    public float Duration => duration;
    public bool IsRunning => isRunning;

    private void Start()
    {
        currentTime = duration;
        if (startOnAwake)
        {
            StartTimer();
        }
    }

    private void Update()
    {
        if (!isRunning) return;

        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0f) currentTime = 0f;

            CheckMilestones();

            if (currentTime <= 0f)
            {
                EndTimer();
            }
        }
    }

    public void StartTimer()
    {
        if (currentTime <= 0f)
        {
            ResetTimer();
        }

        isRunning = true;
        onTimerStart?.Invoke();
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        isRunning = false;
        currentTime = duration;
        Debug.Log(duration);

        triggered100 = true;
        triggered75 = false;
        triggered50 = false;
        triggered25 = false;
        triggered0 = false;

        if (reset != null)
        {
            UpdateAnim(reset);
        }
        else if (animator != null && !string.IsNullOrEmpty(resetAnimatorTrigger))
        {
            animator.SetTrigger(resetAnimatorTrigger);
        }
        else
        {
            UpdateAnim(hundred);
        }

        on100Percent?.Invoke();

        isRunning = true;
    }

    private void EndTimer()
    {
        isRunning = false;
        currentTime = 0f;
        onTimerEnd?.Invoke();
    }

    private void CheckMilestones()
    {
        float percentage = (currentTime / duration) * 100f;

        if (!triggered100 && percentage >= 99.9f)
        {
            triggered100 = true;
            UpdateAnim(hundred);
            on100Percent?.Invoke();
        }
        else if (!triggered75 && percentage <= 75f && percentage > 50f)
        {
            triggered75 = true;
            UpdateAnim(seventyFive);
            on75Percent?.Invoke();
        }
        else if (!triggered50 && percentage <= 50f && percentage > 25f)
        {
            triggered50 = true;
            UpdateAnim(fifty);
            on50Percent?.Invoke();
        }
        else if (!triggered25 && percentage <= 25f && percentage > 0f)
        {
            triggered25 = true;
            UpdateAnim(twentyFive);
            on25Percent?.Invoke();
        }
        else if (!triggered0 && percentage <= 0f)
        {
            triggered0 = true;
            UpdateAnim(zero);
            on0Percent?.Invoke();
        }
    }

    private void UpdateAnim(AnimationClip clip)
    {
        if (animator != null && clip != null)
        {
            animator.Play(clip.name);
        }
    }
}