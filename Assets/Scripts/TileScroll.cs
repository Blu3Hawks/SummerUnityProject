using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float speed = 5f;          // Speed at which tiles move backwards
    public float resetPositionZ = -10f; // Position Z where the tile resets to the front
    public float startPositionZ = 10f;  // Position Z to move the tile back to

    private void Update()
    {
        // Move the tile backward
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Check if the tile has moved past the reset position
        if (transform.position.z <= resetPositionZ)
        {
            // Move the tile back to the start position
            Vector3 newPosition = transform.position;
            newPosition.z = startPositionZ;
            transform.position = newPosition;
        }
    }
}