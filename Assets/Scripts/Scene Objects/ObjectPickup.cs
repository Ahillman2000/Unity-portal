using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float pickupRange = 2f;

    private GrabbableObject grabbedObject;

    private PlayerInputActions playerInputActions;

    private void Start()
    {
        playerInputActions = InputManager.Instance.playerInputActions;

        playerInputActions.Player.PickupDrop.performed += PickupDropObject;
    }

    private void PickupDropObject(InputAction.CallbackContext obj)
    {
        if(grabbedObject == null)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, pickupRange/*, pickupLayer*/))
            {
                if (hit.transform.TryGetComponent(out grabbedObject))
                {
                    grabbedObject.Grab(grabPoint);
                }
            }
        }
        else
        {
            grabbedObject.Drop();
            grabbedObject = null;
        }
    }

    private void OnDisable()
    {
        playerInputActions.Player.PickupDrop.performed -= PickupDropObject;
    }
}
