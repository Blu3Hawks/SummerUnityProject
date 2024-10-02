using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float rotateSpeed = 100f;  //rotation speed for obstacle
    public TileMovement tileMovement;
    private Rigidbody rb;

    private void Start()
    {
        if (tileMovement == null)
        {
            tileMovement = GetComponentInParent<TileMovement>();  //get tilemovement from parent
        }

        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    private void Update()
    {
        if (rb != null)
        {
            Vector3 movePosition = transform.position + Vector3.back * tileMovement.speed * Time.deltaTime;
            rb.MovePosition(movePosition); //move using physics to prevent falling through terrain
        }

        //transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);  //rotate the obstacle

        if (transform.position.z <= tileMovement.resetPositionZ) //destroy obstacle with tile
        {
            Destroy(gameObject);
        }
    }
}
