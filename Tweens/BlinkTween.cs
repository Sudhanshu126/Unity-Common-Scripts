using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlinkTween : MonoBehaviour
{
    //Events
    public UnityEvent<bool> onBlink;

    //Exposed private fields
    [Header("References")]
    [SerializeField] private Image image;

    [Header("Blink tween settings")]
    [SerializeField] private Vector2 blinkRange;
    [SerializeField] private Color activeColor, inactiveColor;
    [SerializeField] private float fBlinkTime, sBlinkTime, reBlinkTime;

    //Private fields
    private float nextBlinkCounter;
    private BlinkStage blinkStage;

    //---MonoBehaviour methods---
    private void Start()
    {
        nextBlinkCounter = Random.Range(blinkRange.x, blinkRange.y);
    }

    private void Update()
    {
        Counter();
    }

    //---Class methods
    //Countdown next blink event
    private void Counter()
    {
        nextBlinkCounter -= Time.deltaTime;
        if(nextBlinkCounter <= 0 )
        {
            CounterEnded();
        }
    }

    //Runs when counter ends, sets blink effect
    private void CounterEnded()
    {
        switch (blinkStage)
        {
            case BlinkStage.IsWaiting:
                image.color = inactiveColor;
                nextBlinkCounter = fBlinkTime;
                blinkStage = BlinkStage.FirstBlink;
                onBlink?.Invoke(false);
                break;

            case BlinkStage.FirstBlink:
                image.color = activeColor;
                nextBlinkCounter = reBlinkTime;
                blinkStage = BlinkStage.IsRestarting;
                onBlink?.Invoke(true);
                break;

            case BlinkStage.IsRestarting:
                image.color = inactiveColor;
                nextBlinkCounter = sBlinkTime;
                blinkStage = BlinkStage.SecondBlink;
                onBlink?.Invoke(false);
                break;

            case BlinkStage.SecondBlink:
                image.color = activeColor;
                nextBlinkCounter = Random.Range(blinkRange.x, blinkRange.y);
                blinkStage = BlinkStage.IsWaiting;
                onBlink?.Invoke(true);
                break;
        }
    }
}
