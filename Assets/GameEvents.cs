using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Current { get; private set; }

    private void Awake()
    {
        if (Current == null)
        {
            Current = this;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning($"There should only be one instance of {Current.GetType()}");
        }
    }

    public event Action<int> OnDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        if(OnDoorwayTriggerEnter != null)
        {
            OnDoorwayTriggerEnter(id);
        }
    }

    public event Action<int> OnDoorwayTriggerExit;
    public void DoorwayRiggerExit(int id)
    {
        if (OnDoorwayTriggerExit != null)
        {
            OnDoorwayTriggerExit(id);
        }
    }
}
