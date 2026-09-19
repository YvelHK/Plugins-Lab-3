using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float offset;
    InputAction move;
    Camera cam;
    Vector2 moveInput;
    float camWidth;

    private void Start()
    {
        // Get camera width
        cam = Object.FindAnyObjectByType<Camera>();
        camWidth = cam.aspect * cam.orthographicSize;

        // Set move action
        move = InputSystem.actions.FindAction("Move");
        move.performed += SetMovement;
        move.canceled += SetMovement;
    }

    private void Update()
    {
        // If not leaving camera boundaries, translate in input direction
        if (IsInCamera((Vector2)transform.position + moveInput))
            transform.Translate(moveInput.x, 0, 0);

        // Otherwise, move to the edge of the screen
        else if (moveInput.x < 0)
            transform.position = new Vector3(cam.transform.position.x - camWidth + offset, transform.position.y, transform.position.z);
        else if (moveInput.x > 0)
            transform.position = new Vector3(cam.transform.position.x + camWidth - offset, transform.position.y, transform.position.z);
    }

    private void SetMovement(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>() * speed * 0.001f;
    }

    private bool IsInCamera(Vector2 newPos)
    {
        // Check if the new move would be leaving the camera
        if (newPos.x < cam.transform.position.x - camWidth + offset || newPos.x > cam.transform.position.x + camWidth - offset)
            return false;
        return true;
    }
}
