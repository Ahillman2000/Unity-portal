using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public CharacterController characterController;
    GameObject mainCamera;

    public float movement_speed = 10f;

    public float mouseSensitivity = 100f;
    float mouse_x_rotation = 0f;
    public float min_camera_clamp_value = -90f;
    public float max_camera_clamp_value = 90f;

    public Transform groundCheckObject;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float player_weight = 1f;
    public float jump_height = 2f;
    readonly float GRAVITY = -9.8f;
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraRotation();
        PlayerMovement();

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump_height * -2f * GRAVITY);
        }

        Gravity();
        GroundCheck();
    }

    private void Gravity()
    {
        velocity.y += GRAVITY * player_weight * Time.deltaTime;
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
    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * movement_speed * Time.deltaTime);
    }
    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        mouse_x_rotation -= mouseY;
        mouse_x_rotation = Mathf.Clamp(mouse_x_rotation, min_camera_clamp_value, max_camera_clamp_value);

        mainCamera.transform.localRotation = Quaternion.Euler(mouse_x_rotation, 0f, 0f);
        this.transform.Rotate(Vector3.up * mouseX);
    }
}
