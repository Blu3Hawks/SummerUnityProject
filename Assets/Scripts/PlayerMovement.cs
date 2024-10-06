using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private GameObject targetObject;
    [SerializeField] private float slerpTimer;

    private Vector3 startPosition;
    private float currentTime;


    void Start()
    {
        startPosition = transform.position;
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        float t = currentTime / slerpTimer;
        if (targetObject == null) { return; }
        else
        {
            transform.position = Vector3.Lerp(startPosition, targetObject.transform.position, t);
        }

        // Get the current acceleration data from the accelerometer
        Vector3 acceleration = Input.acceleration;

        // Detect orientation based on accelerometer data
        if (Mathf.Abs(acceleration.x) > Mathf.Abs(acceleration.y))
        {
            if (acceleration.x > 0)
            {
                Debug.Log("Landscape Left");
            }
            else
            {
                Debug.Log("Landscape Right");
            }
        }
        else
        {
            if (acceleration.y > 0)
            {
                Debug.Log("Portrait Upside Down");
            }
            else
            {
                Debug.Log("Portrait");
            }
        }
    }
}
