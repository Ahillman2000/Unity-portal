using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmancipationGrid : MonoBehaviour
{
    GameObject player;
    SpawnPortalClass spawnPortalScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPortalScript = player.GetComponent<SpawnPortalClass>();
    }

    void OnTriggerEnter()
    {
        spawnPortalScript.DestroyPortals();
    }

    void Update()
    {
        
    }
}
