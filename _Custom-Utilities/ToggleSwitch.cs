using System;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    //Events
    public event Action<bool> onToggle;

    //Properties
    [field: SerializeField] public bool CurrentValue { get; private set; }

    //Exposed private fields
    [Header("References")]
    [SerializeField] private RectTransform tip;
    [SerializeField] private RectTransform switchOnBackground;

    [Header("Toggle switch settings")]
    [SerializeField] private float switchSpeed;

    //Private fields
    private bool needSwitching;
    private float rate;

    //---MonoBehaviour methods---
    private void Start()
    {
        SetSwitchRate();
    }

    private void Update()
    {
        if (needSwitching)
        {
            AnimateSwitch();
        }
    }

    //---Class methods---
    //Switches on and off
    public void Switch()
    {
        CurrentValue = !CurrentValue;

        onToggle?.Invoke(CurrentValue);

        SetSwitchRate();
    }

    //Sets the value and condition of switch
    public void SetSwitchValue(bool value)
    {
        if (value == CurrentValue) { return; }

        CurrentValue = value;
        SetSwitchRate();
    }

    //---UI methods---
    //Sets switch animation
    private void SetSwitchRate()
    {
        if (CurrentValue)
        {
            rate = 1f;
        }
        else
        {
            rate = -1f;
        }
        needSwitching = true;
    }

    //Animates switch
    private void AnimateSwitch()
    {
        Vector2 anchorPivot = tip.pivot;
        anchorPivot.x += switchSpeed * rate * Time.deltaTime;
        anchorPivot.y = 0.5f;

        tip.anchorMin = tip.anchorMax = tip.pivot = anchorPivot;
        switchOnBackground.anchorMax = new Vector2(anchorPivot.x, 1f);

        if(tip.pivot.x < 0f)
        {
            tip.anchorMin = tip.anchorMax = tip.pivot = new Vector2(0f, 0.5f);
            switchOnBackground.anchorMax = new Vector2(0f, 1f);
            needSwitching = false;
        }
        else if(tip.pivot.x > 1f)
        {
            tip.anchorMin = tip.anchorMax = tip.pivot = new Vector2(1f, 0.5f);
            switchOnBackground.anchorMax = new Vector2(1f, 1f);
            needSwitching = false;
        }
    }
}
