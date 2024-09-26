using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRoller : MonoBehaviour
{
    public float rotateSpeed = 100f;  //  rotation speed for spikeroller
    public TileMovement tileMovement;  

    private void Start()
    {
        if (tileMovement == null)
        {
            tileMovement = GetComponentInParent<TileMovement>();  //  get tilemovement from parent
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.back * tileMovement.speed * Time.deltaTime); //move spike roller along with the roadtiles

        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);  //rotate the spile roller

        if (transform.position.z <= tileMovement.resetPositionZ) //  destroy spike roller with tile
        {
            Destroy(gameObject);
        }
    }
}
