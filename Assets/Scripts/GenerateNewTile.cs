using UnityEngine;

public class GenerateNewTile : MonoBehaviour
{
    public GameObject NewTile;
    public Vector3 spawnPosition = new Vector3 (0, 0, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(NewTile, spawnPosition, Quaternion.identity);
        }
    }
}
