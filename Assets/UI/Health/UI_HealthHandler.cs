using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_HealthHandler : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Health")]
    public GameObject[] healthObjects;
    public int currentHealth;
    [SerializeField] private int maxHealth = 3;

    [Header("Rotation & Position")]
    [SerializeField] private int rotationSpeed;
    [SerializeField] private float spaceBetweenObjects;

    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Transform healthObjectParent;


    private Vector3[] originalPositions; // Store the original positions

    private void Start()
    {
        currentHealth = maxHealth;
        originalPositions = new Vector3[healthObjects.Length];
        PositionObjects();
    }

    private void Update()
    {
        RotateHealth();
        CheckForRotations();
    }

    private void RotateHealth()
    {
        foreach (GameObject objectToRotate in healthObjects)
        {
            Vector3 rotation = new Vector3(0, rotationSpeed, 0);
            objectToRotate.transform.Rotate(rotation * Time.deltaTime);
        }
    }

    private void PositionObjects()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        Vector3 topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane + 5f));

        float dynamicSpaceBetweenObjects = spaceBetweenObjects * aspectRatio;
        for (int i = 0; i < healthObjects.Length; i++)
        {
            GameObject gameObject = healthObjects[i];

            Vector3 position = new Vector3(
                topRightCorner.x - (dynamicSpaceBetweenObjects * (i + offsetPosition.x)),
                topRightCorner.y - offsetPosition.y,
                topRightCorner.z - offsetPosition.z
            );

            gameObject.transform.position = position;
            healthObjects[i].transform.localScale = Vector3.one * (aspectRatio / 2);
            gameObject.transform.SetParent(healthObjectParent, true);

            originalPositions[i] = position;
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
                //Debug.Log("Portrait");
                RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
        }
    }

    private void RepositionUIElements(Vector3 addedPos)
    {
        for (int i = 0; i < healthObjects.Length; i++)
        {
            GameObject healthObject = healthObjects[i];

            // Apply the offset (temporary movement)
            Vector3 offsetPosition = originalPositions[i] - addedPos;
            healthObject.transform.position = offsetPosition;

            // Smoothly move back to the original position using DOTween
            healthObject.transform.DOMove(originalPositions[i], 1f);
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth < 0)
        {
            Debug.Log("You lost the game"); // Restart the game logic
        }
        UpdateHealthObjects();
    }

    public void HealUp(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthObjects();
    }

    private void UpdateHealthObjects()
    {
        for (int i = 0; i < healthObjects.Length; i++)
        {
            healthObjects[i].SetActive(i < currentHealth);
        }
    }
}
