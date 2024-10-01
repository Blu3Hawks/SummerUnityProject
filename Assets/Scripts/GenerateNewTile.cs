using UnityEngine;

public class GenerateNewTile : MonoBehaviour
{
    public GameObject NewTile;
    public Vector3 spawnPosition = new Vector3 (0, 0, 0);

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Trigger"))
        {
            Instantiate(NewTile, spawnPosition, Quaternion.identity);
        }
    }
}
