using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private Camera cam;

    private Vector2 moveDirection;
    public Vector2 MoveDirection { get => moveDirection; }

    private Vector3 lookDirection;
    public Vector3 LookDirection { get => lookDirection; }

    public void Init(Player player)
    {
        this.player = player;
        cam = Camera.main;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveDirection = context.ReadValue<Vector2>();
            moveDirection = moveDirection.normalized;
        }

        else if (context.phase == InputActionPhase.Canceled)
        {
            moveDirection = Vector2.zero;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            player.SetRun();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            player.SetRun();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 mouseScreenPos = context.ReadValue<Vector2>();

        Ray ray = cam.ScreenPointToRay(mouseScreenPos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float point))
        {
            Vector3 hitPoint = ray.GetPoint(point);
            Vector3 direction = hitPoint - transform.position;
            direction.y = 0;

            if (direction.magnitude < 0.9f)
            {
                lookDirection = Vector3.zero;
            }
            else
            {
                lookDirection = direction.normalized;
            }
        }
    }
}
