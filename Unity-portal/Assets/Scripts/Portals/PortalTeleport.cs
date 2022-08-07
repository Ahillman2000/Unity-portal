using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PortalTeleport : MonoBehaviour
{
    private GameObject player;
    private SpawnPortal portalSpawner;

    private GameObject otherPortal;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        portalSpawner = player.GetComponent<SpawnPortal>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player && portalSpawner.portalLeftInstance != null && portalSpawner.portalRightInstance != null)
        {
            if (this.CompareTag("PortalBlue"))
            {
                otherPortal = GameObject.FindGameObjectWithTag("PortalRed");
            }
            else if (this.CompareTag("PortalRed"))
            {
                otherPortal = GameObject.FindGameObjectWithTag("PortalBlue");
            }

            Quaternion relativeRotation = Quaternion.Inverse(this.transform.rotation) * collider.transform.rotation;
            relativeRotation *= Quaternion.Euler(0.0f, 180.0f, 0.0f);

            collider.transform.SetPositionAndRotation(otherPortal.transform.position + otherPortal.transform.forward * 1, otherPortal.transform.rotation * relativeRotation);
        }
    }
}