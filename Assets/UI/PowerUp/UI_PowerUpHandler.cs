using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PowerUpHandler : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Power Up")]
    public GameObject[] powerUpObjects;

    [Header("Rotation & Position")]
    [SerializeField] private int rotationSpeed;
    [SerializeField] private float spaceBetweenObjects;

    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Transform powerUpObjectParent;

    private Vector3[] originalPositions; // Store the original positions

    private void Start()
    {
        originalPositions = new Vector3[powerUpObjects.Length];
        PositionObjects();
    }

    private void Update()
    {
        RotatePowerUp();
        CheckForRotations();
    }

    private void CheckForRotations()
    {
        Vector3 acceleration = Input.acceleration;

        if (Mathf.Abs(acceleration.x) > Mathf.Abs(acceleration.y))
        {
            if (acceleration.x > 0)
            {
                Debug.Log("Landscape Left");
                RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
            else
            {
                Debug.Log("Landscape Right");
                RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
        }
        else
        {
            if (acceleration.y > 0)
            {
                Debug.Log("Portrait Upside Down");
                RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
            else
            {
                //                Debug.Log("Portrait");
                RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
        }
    }

    private void RepositionUIElements(Vector3 addedPos)
    {
        for (int i = 0; i < powerUpObjects.Length; i++)
        {
            GameObject powerUpObject = powerUpObjects[i];

            // Apply the offset (temporary movement)
            Vector3 offsetPosition = originalPositions[i] - addedPos;
            powerUpObject.transform.position = offsetPosition;

            // Smoothly move back to the original position using DOTween
            powerUpObject.transform.DOMove(originalPositions[i], 1f);
        }
    }

    private void PositionObjects()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        Vector3 topLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane + 5f));

        float dynamicSpaceBetweenObjects = spaceBetweenObjects * aspectRatio;
        for (int i = 0; i < powerUpObjects.Length; i++)
        {
            GameObject gameObject = powerUpObjects[i];

            Vector3 position = new Vector3(
                topLeftCorner.x + (dynamicSpaceBetweenObjects * (i + offsetPosition.x)),
                topLeftCorner.y - offsetPosition.y,
                topLeftCorner.z - offsetPosition.z
            );

            gameObject.transform.position = position;
            powerUpObjects[i].transform.localScale = Vector3.one * (aspectRatio / 2);
            gameObject.transform.SetParent(powerUpObjectParent, true);

            originalPositions[i] = position;
        }
    }

    private void RotatePowerUp()
    {
        foreach (GameObject objectToRotate in powerUpObjects)
        {
            Vector3 rotation = new Vector3(0, rotationSpeed, 0);
            objectToRotate.transform.Rotate(rotation * Time.deltaTime);
        }
    }
}
