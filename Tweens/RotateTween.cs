using UnityEngine;

public class RotateTween : MonoBehaviour
{
    //Exposed private fields
    [Header("Rotate Tween settings")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool canRotate;

    //---MonoBehaviour methods---
    private void Update()
    {
        if (canRotate)
        {
            transform.Rotate(Vector3.forward , rotationSpeed * Time.deltaTime);
        }
    }
}
