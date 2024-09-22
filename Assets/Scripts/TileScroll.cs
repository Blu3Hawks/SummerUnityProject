using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float speed = 0f;          // Speed at which tiles move backwards
    public float resetPositionZ = 0f; // Position Z where the tile gets destroyed

    private void Update()
    {
        // Move the tile backward
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Check if the tile has moved past the reset position
        if (transform.position.z <= resetPositionZ)
        {
            Destroy(gameObject);
        }
    }
}