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
    public GameObject portalLeftPrefab;
    public GameObject portalRightPrefab;

    [Header("Portal Render Target Textures")]
    public Material portalLeftActiveMaterial;
    public Material portalRightActiveMaterial;

    // instances of each portal once created
    [HideInInspector] public GameObject portalLeftInstance;
    [HideInInspector] public GameObject portalRightInstance;

    // the current material attached to the portals
    private Material portalLeftCurrentMaterial;
    private Material portalRightCurrentMaterial;

    [Header("")]
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
                if (portalLeftInstance != null)
                {
                    Destroy(portalLeftInstance);
                }

                portalLeftInstance = Instantiate(portalLeftPrefab, hit.point, hitObjectRotation);
            }
            else if (portalID == 1)
            {
                if (portalRightInstance != null)
                {
                    Destroy(portalRightInstance);
                }

                portalRightInstance = Instantiate(portalRightPrefab, hit.point, hitObjectRotation);
            }

                    crosshairs.SetPortalCrosshairActive(portalID);
        }
    }

    /// <summary>
    /// Sets the material of the portals to either black or a render texture based on portals in scene
    /// </summary>
    private void PortalMaterials()
    {
        if (portalLeftInstance != null)
        {
            portalLeftCurrentMaterial = portalLeftInstance.GetComponent<Renderer>().material;
        }
        if (portalRightInstance != null)
        {
            portalRightCurrentMaterial = portalRightInstance.GetComponent<Renderer>().material;
        }

        // if left exists and right doesnt
        if (portalLeftInstance != null && portalRightInstance == null)
        {
            // blue = black
            portalLeftCurrentMaterial.color = Color.black;
        }
        // if right exists and left doesnt
        if (portalRightInstance != null && portalLeftInstance == null)
        {
            // red = black
            portalRightCurrentMaterial.color = Color.black;
        }
        // if both exist
        if (portalRightInstance != null && portalLeftInstance != null)
        {
            // blue = blue
            portalLeftCurrentMaterial = portalLeftActiveMaterial;
            // red = red
            portalRightCurrentMaterial = portalRightActiveMaterial;
        }
    }

    /// <summary>
    /// Destroys both any instances of portals in scene
    /// </summary>
    public void DestroyPortals()
    {
        Debug.Log("Portals Destroyed");

        if(portalLeftInstance != null)
        {
            Destroy(portalLeftInstance);
        }
        if(portalRightInstance != null)
        {
            Destroy(portalRightInstance);
        }
        crosshairs.DisableCrosshairs();
    }
}
