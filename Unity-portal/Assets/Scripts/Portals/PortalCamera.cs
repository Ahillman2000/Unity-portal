using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    SpawnPortalClass portalSpawner;

    GameObject Player;
    Camera playerCam;

    GameObject otherPortal;
    Camera portalCam;
    Transform otherPortalCam;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerCam = Camera.main;

        portalSpawner = Player.GetComponent<SpawnPortalClass>();
    }

    private void LateUpdate()
    {
        if (portalSpawner.portalBlueInstance != null && portalSpawner.portalRedInstance != null)
        {
            if (this.CompareTag("PortalBlue"))
            {
                otherPortal = GameObject.FindGameObjectWithTag("PortalRed");
            }
            else if (this.CompareTag("PortalRed"))
            {
                otherPortal = GameObject.FindGameObjectWithTag("PortalBlue");
            }

            portalCam = this.GetComponentInChildren<Camera>();
            otherPortalCam = otherPortal.transform.GetChild(0);

            SetPortalCamPositionAndRotation();
        }
    }

    private void SetPortalCamPositionAndRotation()
    {
        
        //Sebastian Lague implementation
        Matrix4x4 m = otherPortal.transform.localToWorldMatrix 
                      * this.transform.worldToLocalMatrix 
                      * playerCam.transform.localToWorldMatrix;

        Quaternion rotation = Quaternion.Euler(0, 180, 0) * m.rotation;
        //otherPortalCam.rotation = rotation;

        Vector3 relativePos = this.transform.InverseTransformPoint(Player.transform.position);
        relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
        Vector3 position = otherPortal.transform.TransformPoint(relativePos);

        //otherPortalCam.SetPositionAndRotation(m.GetColumn (3), rotation);
        otherPortalCam.SetPositionAndRotation(position, rotation);


        ////////////////////////
        /// Daniel Illet ///////
        ////////////////////////
        /*
        Vector3 relativePos = this.transform.InverseTransformPoint(Player.transform.position);
        relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
        otherPortalCam.transform.position = otherPortal.transform.TransformPoint(relativePos);

        Quaternion relativeRot = Quaternion.Inverse(this.transform.rotation) * Player.transform.rotation;
        relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
        otherPortalCam.transform.rotation = otherPortal.transform.rotation * relativeRot;*/
    }
}