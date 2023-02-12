using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private float speed = 1f;

    private float startHeight;
    private float endHeight;

    void Start()
    {
        GameEvents.Current.OnDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.Current.OnDoorwayTriggerExit  += OnDoorwayClose;

        startHeight = this.transform.position.y;
        endHeight = startHeight + 3f;
    }

    private void OnDoorwayOpen(int id)
    {
        if(this.id == id)
        {
            LeanTween.moveLocalY(this.gameObject, endHeight, speed).setEaseOutQuad();
        }
    }

    private void OnDoorwayClose(int id)
    {
        if (this.id == id)
        {
            LeanTween.moveLocalY(this.gameObject, startHeight, speed).setEaseInQuad();
        }
    }

    private void OnDestroy()
    {
        GameEvents.Current.OnDoorwayTriggerEnter -= OnDoorwayOpen;
        GameEvents.Current.OnDoorwayTriggerExit  -= OnDoorwayClose;
    }
}
