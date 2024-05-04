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

        transform.position = Vector3.Lerp(moveDirection, moveSpeed, Time.deltaTime);
    }
}