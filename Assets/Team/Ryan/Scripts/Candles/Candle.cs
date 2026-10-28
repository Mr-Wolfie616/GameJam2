using System.Collections;
using UnityEngine;

// put this on each candle, it plays the idle and extinguish animations by swapping sprites
// the CandlesOutEvent finds every candle in the scene and blows them out
public class Candle : MonoBehaviour
{
    public enum CandleState
    {
        Lit,
        Extinguishing,
        Out
    }

    public CandleState currentState = CandleState.Lit;

    [Header("Sprites")]
    public SpriteRenderer candleRenderer;      // uses the one on this object if left empty
    public Sprite[] idleSprites = new Sprite[5];
    public Sprite[] extinguishSprites = new Sprite[3];   // last one is the candle off/extinguished
    public float framesPerSecond = 8f;

    [Header("Lighting")]
    // any lights on the candle, turned off when it goes out and back on when relit
    public Light[] candleLights;

    // the event listens to this so it knows when a candle gets relit
    public System.Action onRelit;

    private Coroutine currentAnimation;

    void Start()
    {
        if (candleRenderer == null)
        {
            candleRenderer = GetComponent<SpriteRenderer>();
        }

        // candles start lit
        Relight();
    }

    public bool IsLit()
    {
        return currentState == CandleState.Lit;
    }

    public bool IsOut()
    {
        return currentState == CandleState.Out;
    }

    public void Extinguish()
    {
        if (currentState != CandleState.Lit)
        {
            return;
        }

        currentState = CandleState.Extinguishing;
        PlayAnimation(PlayExtinguish());
    }

    public void Relight()
    {
        currentState = CandleState.Lit;
        SetLights(true);
        PlayAnimation(PlayIdle());

        if (onRelit != null)
        {
            onRelit();
        }
    }

    // stops whatever animation is playing and starts the new one
    private void PlayAnimation(IEnumerator animation)
    {
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(animation);
    }

    // loops the idle sprites forever while the candle is lit
    private IEnumerator PlayIdle()
    {
        int frame = 0;

        while (true)
        {
            candleRenderer.sprite = idleSprites[frame];
            frame = (frame + 1) % idleSprites.Length;
            yield return new WaitForSeconds(1f / framesPerSecond);
        }
    }

    // plays the extinguish sprites once and stays on the last one
    private IEnumerator PlayExtinguish()
    {
        for (int i = 0; i < extinguishSprites.Length; i++)
        {
            candleRenderer.sprite = extinguishSprites[i];

            // lights go off on the last sprite when the candle is fully out
            if (i == extinguishSprites.Length - 1)
            {
                SetLights(false);
            }

            yield return new WaitForSeconds(1f / framesPerSecond);
        }

        currentState = CandleState.Out;
    }

    private void SetLights(bool on)
    {
        foreach (Light candleLight in candleLights)
        {
            if (candleLight != null)
            {
                candleLight.enabled = on;
            }
        }
    }

    // test only - right click the component header
    [ContextMenu("Test: Extinguish")]
    private void TestExtinguish()
    {
        Extinguish();
    }
}
