using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float speed = 0f;          //speed at which tiles move backwards
    public float resetPositionZ = 0f; //position Z where the tile gets destroyed
    
    private void Update()
    {        
        transform.Translate(Vector3.back * speed * Time.deltaTime); //move the tile backward

        if (transform.position.z <= resetPositionZ) //check if the tile has moved past the reset position
        {
            Destroy(gameObject);
        }

    }
}