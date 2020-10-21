using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
}
