using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        PortalPlacement();
    }
}
