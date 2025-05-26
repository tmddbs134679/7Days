using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerData playerData;
    private Camera cam;

    [Header("MoveMent")]
    private float moveSpeed;
    private Vector2 moveDirection;
    public Vector2 MoveDirection { get => moveDirection; }

    [Header("Look")]
    private Vector3 lookDirection;
    public Vector3 LookDirection { get => lookDirection; }

    public void Init(PlayerData playerData)
    {
        this.playerData = playerData;

        _rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main;
        moveSpeed = playerData.walkSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Rotate();
    }

    private void Move()
    {
        Vector3 direction = Vector3.right * moveDirection.x + Vector3.forward * moveDirection.y;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = direction.x * moveSpeed;
        velocity.z = direction.z * moveSpeed;

        _rigidbody.velocity = velocity;
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
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
