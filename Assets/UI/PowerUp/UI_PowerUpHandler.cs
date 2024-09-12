using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PowerUpHandler : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Power Up")]
    public GameObject[] powerUpObject;


    [Header("Rotation & Position")]
    [SerializeField] private int rotationSpeed;
    [SerializeField] private float spaceBetweenObjects;

    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Transform powerUpObjectParent;

    private void Start()
    {
        PositionObjects();
    }

    private void Update()
    {
        RotatePowerUp();
    }

    private void PositionObjects()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        Vector3 topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane + 5f));

        float dynamicSpaceBetweenObjects = spaceBetweenObjects * aspectRatio;
        for (int i = 0; i < powerUpObject.Length; i++)
        {
            GameObject gameObject = powerUpObject[i];

            Vector3 position = new Vector3(
                topRightCorner.x + (dynamicSpaceBetweenObjects * (i + offsetPosition.x)),
                topRightCorner.y - offsetPosition.y,
                topRightCorner.z - offsetPosition.z
            );
            gameObject.transform.position = position;
            powerUpObject[i].transform.localScale = Vector3.one * (aspectRatio / 2);
            gameObject.transform.SetParent(powerUpObjectParent, true);
        }
    }
    private void RotatePowerUp()
    {
        foreach (GameObject objectToRotate in powerUpObject)
        {
            Vector3 rotation = new Vector3(0, rotationSpeed, 0);
            objectToRotate.transform.Rotate(rotation * Time.deltaTime);
        }
    }
}
