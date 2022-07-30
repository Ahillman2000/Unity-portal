using UnityEngine;

public class EmancipationGrid : MonoBehaviour
{
    private GameObject player;
    private SpawnPortal spawnPortalScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPortalScript = player.GetComponent<SpawnPortal>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            Debug.Log("Touched emancipation grid");
            spawnPortalScript.DestroyPortals();
        }
    }
}
