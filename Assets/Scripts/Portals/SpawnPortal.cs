using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPortal : MonoBehaviour
{
    // Instances of each portal once created
    [HideInInspector] public GameObject portalLeftInstance;
    [HideInInspector] public GameObject portalRightInstance;

    [Header("Portal Prefabs")]
    [SerializeField] private GameObject portalLeftPrefab;
    [SerializeField] private GameObject portalRightPrefab;

    [Header("Portal Render Target Textures")]
    [SerializeField] private Material portalLeftActiveMaterial;
    [SerializeField] private Material portalRightActiveMaterial;

    [Header("Crosshair")]
    [SerializeField] private PortalCrosshair crosshairs;

    // the current material attached to the portals
    private Material portalLeftCurrentMaterial;
    private Material portalRightCurrentMaterial;

    private int screenCenterX = Screen.width / 2;
    private int screenCenterY = Screen.height / 2;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = InputManager.Instance.playerInputActions;

        playerInputActions.Player.ShootPortalLeft.performed += ShootPortalLeft;
        playerInputActions.Player.ShootPortalRight.performed += ShootPortalRight;
    }

    void Start()
    {
        screenCenterX = Screen.width / 2;
        screenCenterY = Screen.height / 2;
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
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenCenterX, screenCenterY));

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
