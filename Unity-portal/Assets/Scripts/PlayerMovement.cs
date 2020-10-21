using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;
    GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
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

    void PortalPlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("blue portal");
        }
        if (Input.GetMouseButtonDown(1))
        {
            print("red portal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls();
        PortalPlacement();
    }
}
