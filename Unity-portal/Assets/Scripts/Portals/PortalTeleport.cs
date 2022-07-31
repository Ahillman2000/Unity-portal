using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    private GameObject player;
    private SpawnPortal portalSpawner;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        portalSpawner = player.GetComponent<SpawnPortal>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Touched portal");
        if (collider.gameObject == player && portalSpawner.portalLeftInstance != null && portalSpawner.portalRightInstance != null)
        {
            if (this.CompareTag("PortalBlue"))
            {
                GameObject redPortal = GameObject.FindGameObjectWithTag("PortalRed");

                collider.transform.SetPositionAndRotation(redPortal.transform.position + redPortal.transform.forward * 1, redPortal.transform.rotation);
            }
            else if(this.CompareTag("PortalRed"))
            {
                GameObject bluePortal = GameObject.FindGameObjectWithTag("PortalBlue");

                collider.transform.SetPositionAndRotation(bluePortal.transform.position + bluePortal.transform.forward * 1, bluePortal.transform.rotation);
            }
        }
    }
}