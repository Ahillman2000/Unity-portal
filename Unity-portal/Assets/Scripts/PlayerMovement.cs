using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;
    
    GameObject main_camera;

    public float mouse_x_speed = 2.0f;
    public float mouse_y_speed = 2.0f;

    private float mouse_x = 0.0f;
    private float mouse_y = 0.0f;

    private float player_x_velocity = 0.1f;
    private float player_z_velocity = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        main_camera = GameObject.FindGameObjectWithTag("MainCamera");
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
        mouse_x += mouse_x_speed * Input.GetAxis("Mouse X");
        mouse_y -= mouse_y_speed * Input.GetAxis("Mouse Y");

        // rotation for camera and player
        main_camera.transform.eulerAngles = new Vector3(mouse_y, mouse_x, 0.0f);
        player.transform.Rotate(0, Input.GetAxis("Mouse X") * mouse_x_speed, 0);
    }
}
