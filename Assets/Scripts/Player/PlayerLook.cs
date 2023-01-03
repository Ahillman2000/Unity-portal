using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction look;

    [SerializeField] private float mouseSensitivity = 100f;
    private float mouse_x_rotation = 0;

    [SerializeField] private bool lockedCursor = false;

    private void Start()
    {
        playerInputActions = InputManager.Instance.playerInputActions;
        look = playerInputActions.Player.Look;

        if(lockedCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
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

    void Update()
    {
        CameraRotation();
    }
}
