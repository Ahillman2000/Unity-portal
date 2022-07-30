using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    private PlayerInputActions playerInputActions;
    private InputAction movement;

    [Header("Movement")]
    [SerializeField] private float speed = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckObject;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    [Header("Physics")]
    [SerializeField] private float mass = 1f;
    [SerializeField] private float jumpHeight = 2f;
    readonly float GRAVITY = -9.8f;
    private Vector3 velocity;

    private void Awake()
    {
        playerInputActions = InputManager.Instance.playerInputActions;

        movement = playerInputActions.Player.Movement;
        playerInputActions.Player.Jump.performed += DoJump;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        PlayerMovement();

        Gravity();
        GroundCheck();
    }

    private void PlayerMovement()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        Vector3 move = Camera.main.transform.right * movement.ReadValue<Vector2>().x + Camera.main.transform.forward * movement.ReadValue<Vector2>().y;
        characterController.Move(speed * Time.deltaTime * move);
    }

    private void Gravity()
    {
        velocity.y += GRAVITY * mass * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheckObject.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void DoJump(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * GRAVITY);
        }
    }
}
