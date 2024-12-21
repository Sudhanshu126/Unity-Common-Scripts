using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed, rotationSpeed, shootRange;
    [SerializeField] private LayerMask shootableLayer;
    [SerializeField] private ShootingEffect shootingPrefab;
    [SerializeField] Transform shootPosition;

    private Transform mainCameraTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MyInput.onShooting += OnShooting;
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Vector2 moveInput = MyInput.GetMovementVectorNormalized();
        Vector3 forward = transform.position - new Vector3(mainCameraTransform.position.x, transform.position.y, mainCameraTransform.position.z);
        Vector3 right = mainCameraTransform.right;

        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

        Debug.DrawRay(transform.position, moveDirection * 5f, Color.red);
        HandlePlayerMovementAndRotation(moveDirection);
    }

    private void HandlePlayerMovementAndRotation(Vector3 moveDirection)
    {
        if(moveDirection == Vector3.zero) { return; }

        Vector3 destination = transform.position + moveDirection * moveSpeed;
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }

    private void OnShooting()
    {
        Ray shootAim = new Ray(mainCameraTransform.position, mainCameraTransform.forward);
        Physics.Raycast(shootAim, out RaycastHit hitInfo, shootRange, shootableLayer);

        Vector3 shootDirection = hitInfo.point - shootPosition.position;
        ShootingEffect shootingEffectObject = Instantiate(shootingPrefab, shootPosition.position, shootPosition.rotation);
        shootingEffectObject.Move(50f);
    }
}
