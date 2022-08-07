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
    private InputAction look;

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

    [SerializeField] private float mouseSensitivity = 100f;
    private float mouse_x_rotation = 0;

    private void Awake()
    {
        playerInputActions = InputManager.Instance.playerInputActions;

        movement = playerInputActions.Player.Movement;
        look = playerInputActions.Player.Look;
        playerInputActions.Player.Jump.performed += DoJump;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        PlayerMovement();
        CameraRotation();

        Gravity();
        GroundCheck();
    }

    /// <summary>
    /// faces player in direction of camera and moves in relation to camera
    /// </summary>
    private void PlayerMovement()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        var forward = Camera.main.transform.forward;
        var right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = right * movement.ReadValue<Vector2>().x + forward * movement.ReadValue<Vector2>().y;

        characterController.Move(speed * Time.deltaTime * moveDirection);
    }

    /// <summary>
    /// Rotates the player horizontally and the camera vertically
    /// </summary>
    private void CameraRotation()
    {
        float mouse_x = look.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouse_y = look.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        mouse_x_rotation -= mouse_y;
        mouse_x_rotation = Mathf.Clamp(mouse_x_rotation, -90, 90);

        this.gameObject.transform.Rotate(0, mouse_x, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(mouse_x_rotation, 0, 0);
    }

    /// <summary>
    /// Gravitational force applied to the character controller
    /// </summary>
    private void Gravity()
    {
        velocity.y += GRAVITY * mass * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Check to determine if character controller is touching ground
    /// </summary>
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
