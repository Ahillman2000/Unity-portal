using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    private GameObject Player;
    private Camera playerCam;
    private SpawnPortal portalSpawner;

    private Camera portalCam;
    private GameObject otherPortal;
    private Transform otherPortalCam;

    public float nearClipOffset = 0.05f;
    public float nearClipLimit = 0.2f;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerCam = Camera.main;

        portalSpawner = Player.GetComponent<SpawnPortal>();
    }

    private void LateUpdate()
    {
        if (portalSpawner.portalLeftInstance != null && portalSpawner.portalRightInstance != null)
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
            SetViewFrustrum();
        }
    }

    /// <summary>
    /// Sebastian Lague
    /// </summary>
    private void SetPortalCamPositionAndRotation()
    {
        Matrix4x4 m = otherPortal.transform.localToWorldMatrix 
                      * this.transform.worldToLocalMatrix 
                      * playerCam.transform.localToWorldMatrix;

        Quaternion rotation = Quaternion.Euler(0, 180, 0) * m.rotation;

        Vector3 relativePos = this.transform.InverseTransformPoint(Player.transform.position);
        relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
        Vector3 position = otherPortal.transform.TransformPoint(relativePos);

        otherPortalCam.SetPositionAndRotation(position, rotation);
    }

    /// <summary>
    /// Sebastian Lague
    /// </summary>
    private void SetViewFrustrum()
    {
        Transform clipPlane = transform;
        int dot = System.Math.Sign(Vector3.Dot(clipPlane.forward, transform.position - portalCam.transform.position));

        Vector3 camSpacePos = portalCam.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 camSpaceNormal = portalCam.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float camSpaceDst = -Vector3.Dot(camSpacePos, camSpaceNormal) + nearClipOffset;

        if (Mathf.Abs(camSpaceDst) > nearClipLimit)
        {
            Vector4 clipPlaneCameraSpace = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDst);

            portalCam.projectionMatrix = playerCam.CalculateObliqueMatrix(clipPlaneCameraSpace);
        }
        else
        {
            portalCam.projectionMatrix = playerCam.projectionMatrix;
        }
    }
}