using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public GameObject bluePortal;
    public GameObject redPortal;
    public Camera mainCamera;

    void PortalPlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //print("blue portal");
            ThrowPortal(bluePortal);
        }
        if (Input.GetMouseButtonDown(1))
        {
            //print("red portal");
            ThrowPortal(redPortal);
        }
    }

    void ThrowPortal(GameObject portal)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag("Portalable"))
        {
            print("hit a Portalable surface");
            Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
            portal.transform.position = hit.point;
            portal.transform.rotation = hitObjectRotation;
        }
        else 
        {
            print("hit a non Portalable surface");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PortalPlacement();
    }
}
