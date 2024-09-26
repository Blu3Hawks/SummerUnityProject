using UnityEngine;

public class GenerateNewTile : MonoBehaviour
{
    public GameObject NewTile;
    public Vector3 spawnPosition = new Vector3 (0, 0, 0);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "GenerateNext")
        {
            Instantiate(NewTile, spawnPosition, Quaternion.identity);
        }
    }
}
