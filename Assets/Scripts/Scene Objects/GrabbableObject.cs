using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Rigidbody rb;
    private Transform grabTransform;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void Grab(Transform grabTransform)
    {
        this.grabTransform = grabTransform;
        rb.useGravity = false;
    }

    public void Drop()
    {
        grabTransform = null;
        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        if(grabTransform != null)
        {
            Vector3 newPos = Vector3.Lerp(this.transform.position, grabTransform.position, Time.deltaTime * 10f);
            rb.MovePosition(newPos);
        }
    }
}
