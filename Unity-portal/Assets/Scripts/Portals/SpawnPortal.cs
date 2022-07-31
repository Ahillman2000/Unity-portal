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

    [SerializeField] private PortalCrosshair crosshairs;

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

    /// <summary>
    /// Instantiates the relevant portal object within the scene
    /// </summary>
    /// <param name="portalID"> The ID of the portal to be Spawned (0 = left, 1 = right) </param>
    private void ShootPortal(int portalID)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = mainCamera.ScreenPointToRay(new Vector3(x, y));

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag("Portalable"))
        {
            Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);

            if (portalID == 0)
            {
                if (portalBlueInstance != null)
                {
                    Destroy(portalBlueInstance);
                }

                portalBlueInstance = Instantiate(portalBluePrefab, hit.point, hitObjectRotation);
            }
            else if (portalID == 1)
            {
                if (portalRedInstance != null)
                {
                    Destroy(portalRedInstance);
                }

                portalRedInstance = Instantiate(portalRedPrefab, hit.point, hitObjectRotation);
            }

            crosshairs.SetPortalCrosshairActive(portalID);
        }
    }

    /// <summary>
    /// Sets the material of the portals to either black or a render texture based on portals in scene
    /// </summary>
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

    /// <summary>
    /// Destroys both any instances of portals in scene
    /// </summary>
    public void DestroyPortals()
    {
        Debug.Log("Portals Destroyed");

        if(portalBlueInstance != null)
        {
            Destroy(portalBlueInstance);
        }
        if(portalRedInstance != null)
        {
            Destroy(portalRedInstance);
        }
        crosshairs.DisableCrosshairs();
    }
}
