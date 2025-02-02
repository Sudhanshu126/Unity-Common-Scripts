using UnityEngine;

public class SlideTween : MonoBehaviour
{
    //Exposed private fields
    [Header("References")]
    [SerializeField] private RectTransform slidable;

    [Header("Slide tween settings")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private int slideDirectionX;

    //Private fields
    private bool needSliding;
    private float rate;

    //---MonoBehaviour methods---
    private void Update()
    {
        if (needSliding)
        {
            Slide();
        }
    }

    //---Class methods---
    //Slides the object
    private void Slide()
    {
        Vector2 pivot = slidable.pivot;
        pivot.x += slideSpeed * rate * Time.deltaTime;
        pivot.y = 0.5f;

        if(pivot.x > 1f)
        {
            pivot.x = 1f;
            needSliding = false;
        }
        else if(pivot.x < 0f)
        {
            pivot.x = 0f;
            needSliding = false;
        }

        slidable.pivot = pivot;
    }

    //Sets the trigger for sliding
    public void SlideTrigger(bool open)
    {
        if (open)
        {
            rate = -1f * slideDirectionX;
        }
        else
        {
            rate = 1f * slideDirectionX;
        }

        needSliding = true;
    }
}
