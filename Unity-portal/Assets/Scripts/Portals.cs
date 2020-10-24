using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public GameObject bluePortal;
    public GameObject redPortal;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void PortalPlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("blue portal");
            ThrowPortal(bluePortal);
        }
        if (Input.GetMouseButtonDown(1))
        {
            print("red portal");
            ThrowPortal(redPortal);
        }
    }

    void ThrowPortal(GameObject portal)
    {
        // 4:07
    }

    // Update is called once per frame
    void Update()
    {
        PortalPlacement();
    }
}
