using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_PowerUpHandler : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Power Up")]
    public GameObject powerUpPrefab;
    public List<GameObject> powerUpObjects = new List<GameObject>();

    [Header("Rotation & Position")]
    [SerializeField] private int rotationSpeed;
    [SerializeField] private float spaceBetweenObjects;

    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Transform powerUpObjectParent;

    public int currentPowerUps;
    private Vector3[] originalPositions;

    private void Start()
    {
        originalPositions = new Vector3[10];
        InitializePowerUpObjects();
        PositionObjects();
    }

    private void Update()
    {
        RotatePowerUp();
        CheckForRotations();
    }

    private void InitializePowerUpObjects()
    {
        foreach (Transform child in powerUpObjectParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentPowerUps; i++)
        {
            GameObject powerUpObjectInstance = Instantiate(powerUpPrefab, powerUpObjectParent);
            powerUpObjects.Add(powerUpObjectInstance);
        }
    }

    public void AddPowerup()
    {
        currentPowerUps++;
        GameObject powerUpObjectInstance = Instantiate(powerUpPrefab, powerUpObjectParent);
        powerUpObjects.Add(powerUpObjectInstance);
        PositionObjects(); // Reposition objects after adding
    }

    public void PositionObjects()
    {
        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera is not assigned.");
            return;
        }

        Vector3 topLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane + 5f));

        bool isPortrait = Screen.height > Screen.width;

        for (int i = 0; i < powerUpObjects.Count; i++)
        {
            GameObject powerUpObject = powerUpObjects[i];
            Vector3 position;

            if (!isPortrait)
            {
                position = new Vector3(
                    topLeftCorner.x + offsetPosition.x + (i * spaceBetweenObjects * 2),
                    topLeftCorner.y  - offsetPosition.y,
                    topLeftCorner.z - offsetPosition.z
                );
                powerUpObject.transform.position = position;
                powerUpObject.transform.localScale = Vector3.one * ((float)Screen.width / Screen.height / 6);
                originalPositions[i] = position;
            }
            else
            {
                position = new Vector3(
                    topLeftCorner.x + (i * spaceBetweenObjects) + offsetPosition.x,
                    topLeftCorner.y - offsetPosition.y,
                    topLeftCorner.z - offsetPosition.z
                );
                powerUpObject.transform.position = position;
                powerUpObject.transform.localScale = Vector3.one * ((float)Screen.width / Screen.height / 2);
                originalPositions[i] = position;
            }
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
                RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
        }
    }

    private void RepositionUIElements(Vector3 addedPos)
    {
        for (int i = 0; i < powerUpObjects.Count; i++)
        {
            GameObject powerUpObject = powerUpObjects[i];

            Vector3 offsetPosition = originalPositions[i] - addedPos;
            powerUpObject.transform.position = offsetPosition;

            powerUpObject.transform.DOMove(originalPositions[i], 1f);
        }
    }

    
}
