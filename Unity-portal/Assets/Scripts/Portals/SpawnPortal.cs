using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPortal : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    Camera mainCamera;

    [Header("Portal Prefabs")]
    public GameObject portalBluePrefab;
    public GameObject portalRedPrefab;

    [Header("Portal Render Target Textures")]
    public Material portalBlueActiveMaterial;
    public Material portalRedActiveMaterial;

    // instances of each portal once created
    [HideInInspector] public GameObject portalBlueInstance;
    [HideInInspector] public GameObject portalRedInstance;

    // the current material attached to the portals
    private Material portalBlueCurrentMaterial;
    private Material portalRedCurrentMaterial;

    private void Awake()
    {
        playerInputActions = InputManager.Instance.playerInputActions;

        playerInputActions.Player.ShootPortalLeft.performed += ShootPortalLeft;
        playerInputActions.Player.ShootPortalRight.performed += ShootPortalRight;
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        PortalMaterials();
    }

    private void ShootPortalLeft(InputAction.CallbackContext context)
    {
        ShootPortal(0);
    }

    private void ShootPortalRight(InputAction.CallbackContext context)
    {
        ShootPortal(1);
    }

    private void ShootPortal(int portalID)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag("Portalable"))
        {
            Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);

            if(portalID == 0)
            {
                if (portalRedInstance != null)
                {
                    Destroy(portalRedInstance);
                }

                portalRedInstance = Instantiate(portalRedPrefab, hit.point, hitObjectRotation);
            }
            else if (portalID == 1)
            {
                if (portalBlueInstance != null)
                {
                    Destroy(portalBlueInstance);
                }

                portalBlueInstance = Instantiate(portalBluePrefab, hit.point, hitObjectRotation);
            }
        }
    }

    private void PortalMaterials()
    {
        if (portalBlueInstance != null)
        {
            portalBlueCurrentMaterial = portalBlueInstance.GetComponent<Renderer>().material;
        }
        if (portalRedInstance != null)
        {
            portalRedCurrentMaterial = portalRedInstance.GetComponent<Renderer>().material;
        }

        // if blue exists and red doesnt
        if (portalBlueInstance != null && portalRedInstance == null)
        {
            // blue = black
            portalBlueCurrentMaterial.color = Color.black;
        }
        // if red exists and blue doesnt
        if (portalRedInstance != null && portalBlueInstance == null)
        {
            // red = black
            portalRedCurrentMaterial.color = Color.black;
        }
        // if both exist
        if (portalRedInstance != null && portalBlueInstance != null)
        {
            // blue = blue
            portalBlueCurrentMaterial = portalBlueActiveMaterial;
            // red = red
            portalRedCurrentMaterial = portalRedActiveMaterial;
        }
    }

    public void DestroyPortals()
    {
        Debug.Log("Portals Destroyed");

        Destroy(portalBlueInstance);
        Destroy(portalRedInstance);
    }
}
