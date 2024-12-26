using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform mainCamera;
    private CharacterController controller;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed, turnSpeed, sprintSpeed;
    [SerializeField] private float sprintTransitSpeed;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float jumpHeight;
    private float verticalVelocity, moveSpeed;

    [Header("Input")]
    private Vector2 input;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        InputManagement();
        Movement();
    }

    private void InputManagement()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private float VericalForceCalculation()
    {
        if(controller.isGrounded)
        {
            verticalVelocity = 0f;
            if(Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }

    private void Movement()
    {
        GroundMovement();
        Turn();
    }

    private void GroundMovement()
    {
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        moveDirection = mainCamera.transform.TransformDirection(moveDirection);
        moveDirection.y = VericalForceCalculation();

        if(InputManagement.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, Time.deltaTime);
        }

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Turn()
    {
        if(input == Vector2.zero) { return; }

        Vector3 lookDirection = controller.velocity.normalized;
        lookDirection.y = 0f;
        lookDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
