using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepThroughPortal : MonoBehaviour
{
    public GameObject otherPortal;

    private void OnTriggerEnter(Collider other)
    {
        //print("touching portal");
        if (other.CompareTag("Player") /*|| other.CompareTag("CompanionCube")*/)
        {
            other.transform.position = otherPortal.transform.position + otherPortal.transform.forward * 1;
            other.transform.rotation = otherPortal.transform.rotation;
            
            // Player exit perpendicluar to exit portal,
            // change so that exit angle is relative to entrance angle
        }
    }
}
