using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;
    
    GameObject main_camera;

    public float x_speed = 2.0f;
    public float y_speed = 2.0f;

    private float x = 0.0f;
    private float y = 0.0f;

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
            player.transform.Translate(0, 0, 0.1f);
        }
        if (Input.GetKey("a"))
        {
            //print("A");
            player.transform.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey("s"))
        {
            //print("S");
            player.transform.Translate(0, 0, -0.1f);
        }
        if (Input.GetKey("d"))
        {
            //print("D");
            player.transform.Translate(0.1f, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls();
    }

    private void FixedUpdate()
    {
        x += x_speed * Input.GetAxis("Mouse X");
        y -= y_speed * Input.GetAxis("Mouse Y");

        // rotation for camera and player
        main_camera.transform.eulerAngles = new Vector3(y, x, 0.0f);
        player.transform.Rotate(0, Input.GetAxis("Mouse X") * x_speed, 0);
    }
}
