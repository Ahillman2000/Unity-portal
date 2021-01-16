using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Camera playerCam;

    public GameObject portal;
    public Camera portalCam;

    public GameObject otherPortal;
    public Camera otherPortalCam;

    private void LateUpdate()
    {
        PoralCameraPositions();
        PortalCameraRotations();
    }

    private void PoralCameraPositions()
    {
        /*Vector3 referencePoint = playerCam.transform.position + playerCam.transform.forward * 100;
        Vector3 diff = portal.transform.position - referencePoint;
        Vector3 projectedPoint = otherPortal.transform.position + diff;

        otherPortalCam.transform.position = (diff * 0.1f) + otherPortal.transform.position;

        /*Vector3 playerOffsetFromPortal = portal.transform.position - playerCam.transform.position;
        otherPortalCam.transform.position = otherPortal.transform.position + playerOffsetFromPortal;*/
    }

    private void PortalCameraRotations()
    {
        //Sebastian Lague implementation
        Matrix4x4 m = otherPortal.transform.localToWorldMatrix 
                      * portal.transform.worldToLocalMatrix 
                      * playerCam.transform.localToWorldMatrix;

        Quaternion rotation = Quaternion.Euler(0, 180, 0) * m.rotation;

        otherPortalCam.transform.rotation = rotation;
    }
}