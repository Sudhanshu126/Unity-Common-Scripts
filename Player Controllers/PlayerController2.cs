//Player Controller with lerping position
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ);
        Vector3 destination = tranform.position + moveDirection * moveSpeed;
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
    }
}
