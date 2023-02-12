using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private float speed = 0.1f;
 
    private float startHeight;
    private float endHeight;

    private void Start()
    {
        startHeight = this.transform.position.y;
        endHeight = startHeight - 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEvents.Current.DoorwayTriggerEnter(id);
        LeanTween.moveLocalY(this.gameObject, endHeight, speed).setEaseInQuad();
    }

    private void OnTriggerExit(Collider other)
    {
        GameEvents.Current.DoorwayRiggerExit(id);
        LeanTween.moveLocalY(this.gameObject, startHeight, speed).setEaseOutQuad();
    }
}
