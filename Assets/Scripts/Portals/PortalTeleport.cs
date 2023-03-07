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
        if (this.CompareTag("PortalLeft"))
        {
            otherPortal = GameObject.FindGameObjectWithTag("PortalRight");
        }
        else if (this.CompareTag("PortalRight"))
        {
            otherPortal = GameObject.FindGameObjectWithTag("PortalLeft");
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

        StartCoroutine(DisableCollision(teleportObject, 0.25f));

        // set rotation to face correct direction
        Vector3 currentRotation = rb.transform.eulerAngles;
        currentRotation.x = 0;
        currentRotation.z = 0;

        rb.transform.eulerAngles = currentRotation;
        //rb.transform.eulerAngles = Vector3.Lerp(rb.transform.eulerAngles, currentRotation, 1.0f);
    }

    /// <summary>
    /// Temporarily disables the targets collider
    /// </summary>
    /// <param name="target"> The collider to be temporarily disabled </param>
    /// <param name="disableTime"> The time in seconds for the collider to be disbled </param>
    private IEnumerator DisableCollision(Collider target, float disableTime)
    {
        target.enabled = false;
        yield return new WaitForSeconds(disableTime);
        target.enabled = true;
    }
}