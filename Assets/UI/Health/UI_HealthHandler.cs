using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_HealthHandler : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Health")]
    public GameObject healthPrefab; // Use a single prefab instead of array for instantiation
    private List<GameObject> healthObjects = new List<GameObject>(); // Instances of health objects
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
        originalPositions = new Vector3[maxHealth];
        InitializeHealthObjects();
        PositionObjects();
    }

    private void Update()
    {
        RotateHealth();
        CheckForRotations();
    }

    private void InitializeHealthObjects()
    {
        // Clear any existing children to avoid duplicates
        foreach (Transform child in healthObjectParent)
        {
            Destroy(child.gameObject);
        }

        // Create instances of health objects and add them to the list
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject healthObjectInstance = Instantiate(healthPrefab, healthObjectParent);
            healthObjectInstance.SetActive(i < currentHealth); // Set active only up to current health
            healthObjects.Add(healthObjectInstance);
        }
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
        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera is not assigned.");
            return;
        }
        Vector3 topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane + 5f));

        for (int i = 0; i < healthObjects.Count; i++)
        {
            GameObject healthObject = healthObjects[i];

            Vector3 position = new Vector3(
                topRightCorner.x - (i * spaceBetweenObjects) - offsetPosition.x,
                topRightCorner.y - offsetPosition.y,
                topRightCorner.z - offsetPosition.z
            );

            healthObject.transform.position = position;
            healthObject.transform.localScale = Vector3.one * ((float)Screen.width / Screen.height / 2); // Adjust as needed
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
                // Debug.Log("Portrait");
                RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
        }
    }

    private void RepositionUIElements(Vector3 addedPos)
    {
        for (int i = 0; i < healthObjects.Count; i++)
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
        for (int i = 0; i < healthObjects.Count; i++)
        {
            healthObjects[i].SetActive(i < currentHealth);
        }
    }
}
