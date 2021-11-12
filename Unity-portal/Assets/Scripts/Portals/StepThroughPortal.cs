using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepThroughPortal : MonoBehaviour
{
    GameObject player;
    SpawnPortalClass portalSpawner;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        portalSpawner = player.GetComponent<SpawnPortalClass>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && portalSpawner.portalBlueInstance != null && portalSpawner.portalRedInstance != null)
        {
            Debug.Log("teleport");

            // old transform method
            /*collider.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 1;
              other.transform.rotation = otherPortal.transform.rotation;*/

            if (this.CompareTag("PortalBlue"))
            {
                GameObject redPortal = GameObject.FindGameObjectWithTag("PortalRed");

                collider.transform.position = redPortal.transform.position + redPortal.transform.forward * 1;
                collider.transform.rotation = redPortal.transform.rotation;
            }
            else if(this.CompareTag("PortalRed"))
            {
                GameObject bluePortal = GameObject.FindGameObjectWithTag("PortalBlue");

                collider.transform.position = bluePortal.transform.position + bluePortal.transform.forward * 1;
                collider.transform.rotation = bluePortal.transform.rotation;
            }

            // Player exit perpendicluar to exit portal,
            // change so that exit angle is relative to entrance angle
        }
    }
}
