using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PortalTeleport : MonoBehaviour
{
    private GameObject player;
    private SpawnPortal portalSpawner;

    // The opposite portal to this object
    private GameObject otherPortal;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        portalSpawner = player.GetComponent<SpawnPortal>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Rigidbody>() != null && portalSpawner.BothPortalsSpawned())
        {
            Teleport(collider);
        }
    }

    /// <summary>
    /// Teleports the colliding object to the opposite portal
    /// </summary>
    /// <param name="teleportObject"> The object colliding with the trigger volume </param>
    private void Teleport(Collider teleportObject)
    {
        if (this.CompareTag("PortalBlue"))
        {
            otherPortal = GameObject.FindGameObjectWithTag("PortalRed");
        }
        else if (this.CompareTag("PortalRed"))
        {
            otherPortal = GameObject.FindGameObjectWithTag("PortalBlue");
        }

        // Update relative rotation
        Quaternion relativeRotation = Quaternion.Inverse(this.transform.rotation) * teleportObject.transform.rotation;
        relativeRotation *= Quaternion.Euler(0.0f, 180.0f, 0.0f);

        teleportObject.transform.SetPositionAndRotation(otherPortal.transform.position + otherPortal.transform.forward * 1, otherPortal.transform.rotation * relativeRotation);

        // Update velocity of rigidbody.
        Rigidbody rb = teleportObject.GetComponent<Rigidbody>();
        Vector3 relativeVel = this.transform.InverseTransformDirection(rb.velocity);
        relativeVel = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeVel;
        rb.velocity = otherPortal.transform.TransformDirection(relativeVel);

        StartCoroutine(DisableCollision(teleportObject));
    }

    private IEnumerator DisableCollision(Collider target)
    {
        target.enabled = false;
        yield return new WaitForSeconds(0.50f);
        target.enabled = true;
    }
}