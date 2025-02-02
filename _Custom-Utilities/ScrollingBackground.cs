using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    //Exposed private fields
    [Header("References")]
    [SerializeField] private RawImage rawImage;

    [Header("Scrolling Background settings")]
    [SerializeField] private Vector2 scrollVelocity;

    //---MonoBehaviour methods---
    private void Start()
    {
        //Calibrate velocity
        scrollVelocity.x = scrollVelocity.x / 100;
        scrollVelocity.y = scrollVelocity.y / 100;
    }

    void Update()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + scrollVelocity * Time.deltaTime, rawImage.uvRect.size);
    }
}
