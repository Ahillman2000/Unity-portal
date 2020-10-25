using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public GameObject bluePortal;
    public GameObject redPortal;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
    }

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
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
            portal.transform.position = hit.point;
            portal.transform.rotation = hitObjectRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PortalPlacement();
    }
}
