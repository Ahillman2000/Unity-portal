using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;
    GameObject companionCube;
    GameObject main_camera;

    private float mouse_x = 0.0f;
    private float mouse_y = 0.0f;

    private float mouse_x_rotation = 0;

    private float mouse_sensitivity = 100f;

    public float player_x_velocity;
    public float player_z_velocity;

    public bool cube_pickup = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        main_camera = GameObject.FindGameObjectWithTag("MainCamera");
        companionCube = GameObject.Find("CompanionCube");

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
        mouse_x = Input.GetAxis("Mouse X") * mouse_sensitivity * Time.deltaTime;
        mouse_y = Input.GetAxis("Mouse Y") * mouse_sensitivity * Time.deltaTime;

        mouse_x_rotation -= mouse_y;
        mouse_x_rotation = Mathf.Clamp(mouse_x_rotation, -90, 90);

        player.transform.Rotate(0, mouse_x, 0);
        main_camera.transform.localRotation = Quaternion.Euler(mouse_x_rotation, 0, 0);

        /*if(cube_pickup == true)
        {
            companionCube.transform.parent = player.transform;
        }*/
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CompanionCube"))
        {
            if (Input.GetKeyDown("e") && !cube_pickup)
            {
                // set position to in front of the player
                other.GetComponent<Rigidbody>().useGravity = false;
                cube_pickup = true;
            }
            else if (Input.GetKeyDown("e") && cube_pickup)
            {
                other.transform.parent = null;
                other.GetComponent<Rigidbody>().useGravity = true;
                cube_pickup = false;
            }
        }
    }*/
}
