using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction movement;

    [Header("Componenet References")]
    [SerializeField] private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float movementMultiplier = 10f;

    [SerializeField] private float airMultiplier = 0.4f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 15f;

    private float playerHeight = 2f;

    private Vector3 moveDirection;

    private bool isGrounded;

    private float groundDrag = 6f;
    private float airDrag = 2f;


    void Start()
    {
        playerInputActions = InputManager.Instance.playerInputActions;
        movement = playerInputActions.Player.Movement;
        playerInputActions.Player.Jump.performed += DoJump;
    }

    void Update()
    {
        ControlDrag();

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight/2 + 0.1f);

        moveDirection = transform.forward * movement.ReadValue<Vector2>().y + transform.right * movement.ReadValue<Vector2>().x;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if(isGrounded)
        {
            rb.AddForce(movementMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(movementMultiplier * airMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Acceleration);
        }
    }

    private void DoJump(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ControlDrag()
    {
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.performed -= DoJump;
    }
}
