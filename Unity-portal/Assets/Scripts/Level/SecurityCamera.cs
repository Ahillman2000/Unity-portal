using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public GameObject subject;
    void Update()
    {
        this.transform.LookAt(subject.transform.position);
    }
}
