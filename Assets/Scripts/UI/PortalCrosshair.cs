using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCrosshair : MonoBehaviour
{
    [SerializeField] private GameObject leftPortalCrosshair;
    [SerializeField] private GameObject rightPortalCrosshair;

    void Start()
    {
        DisableCrosshairs();
    }

    /// <summary>
    /// Activates the relevant portal crosshair UI
    /// </summary>
    /// <param name="portalID"> The portal being spawned </param>
    public void SetPortalCrosshairActive(int portalID)
    {
        if(portalID == 0)
        {
            leftPortalCrosshair.SetActive(true);
        }
        else
        {
            rightPortalCrosshair.SetActive(true);
        }
    }

    /// <summary>
    /// Disables both crosshair UI elements
    /// </summary>
    public void DisableCrosshairs()
    {
        leftPortalCrosshair.SetActive(false);
        rightPortalCrosshair.SetActive(false);
    }
}
