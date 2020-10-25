using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;
    GameObject playerBody;
    
    GameObject main_camera;

    private float mouse_x = 0.0f;
    private float mouse_y = 0.0f;

    private float mouse_x_rotation = 0;

    private float mouseSensitivity = 100f;

    private float player_x_velocity = 0.1f;
    private float player_z_velocity = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = GameObject.Find("player_body");
        main_camera = GameObject.FindGameObjectWithTag("MainCamera");

        Cursor.lockState = CursorLockMode.Locked;
    }

    void PlayerControls()
    {
        if (Input.GetKey("w"))
        {
            //print("W");
            player.transform.Translate(0, 0, player_z_velocity);
        }
        if (Input.GetKey("a"))
        {
            //print("A");
            player.transform.Translate(-player_x_velocity, 0, 0);
        }
        if (Input.GetKey("s"))
        {
            //print("S");
            player.transform.Translate(0, 0, -player_z_velocity);
        }
        if (Input.GetKey("d"))
        {
            //print("D");
            player.transform.Translate(player_x_velocity, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls();
    }

    private void FixedUpdate()
    {
        mouse_x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouse_y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        mouse_x_rotation -= mouse_y;
        mouse_x_rotation = Mathf.Clamp(mouse_x_rotation, -90, 90);

        player.transform.Rotate(0, mouse_x, 0);
        main_camera.transform.localRotation = Quaternion.Euler(mouse_x_rotation, 0, 0);

    }
}
