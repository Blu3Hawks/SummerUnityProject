using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_HealthHandler : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Health")]
    public GameObject healthPrefab;
    private List<GameObject> healthObjects = new List<GameObject>();
    public int currentHealth;
    [SerializeField] private int maxHealth = 3;

    [Header("Rotation & Position")]
    [SerializeField] private int rotationSpeed;
    [SerializeField] private float spaceBetweenObjects;

    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Transform healthObjectParent;

    [SerializeField] private LoseMenu lostMenuCanvas;
    private Vector3[] originalPositions;

    private void Start()
    {
        currentHealth = maxHealth;
        originalPositions = new Vector3[maxHealth];
        lostMenuCanvas = FindObjectOfType<LoseMenu>();
        lostMenuCanvas.gameObject.SetActive(false);
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
        if (healthObjectParent.childCount != 0)
        {
            foreach (Transform child in healthObjectParent)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject healthObjectInstance = Instantiate(healthPrefab, healthObjectParent);
            healthObjectInstance.SetActive(i < currentHealth);
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

        bool isPortrait = Screen.height > Screen.width;

        for (int i = 0; i < healthObjects.Count; i++)
        {
            GameObject healthObject = healthObjects[i];
            Vector3 position;

            if (!isPortrait)
            {
                position = new Vector3(
                    topRightCorner.x - offsetPosition.x - (i * spaceBetweenObjects * 2),
                    topRightCorner.y  - offsetPosition.y,
                    topRightCorner.z - offsetPosition.z
                );

                healthObject.transform.position = position;
                healthObject.transform.localScale = Vector3.one * ((float)Screen.width / Screen.height / 6); 
                originalPositions[i] = position;
            }
            else
            {
                position = new Vector3(
                    topRightCorner.x - (i * spaceBetweenObjects) - offsetPosition.x,
                    topRightCorner.y - offsetPosition.y,
                    topRightCorner.z - offsetPosition.z
                );
                healthObject.transform.position = position;
                healthObject.transform.localScale = Vector3.one * ((float)Screen.width / Screen.height / 2); 
                originalPositions[i] = position;
            }
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
        for (int i = 0; i < healthObjects.Count; i++)
        {
            GameObject healthObject = healthObjects[i];

            Vector3 offsetPosition = originalPositions[i] - addedPos;
            healthObject.transform.position = offsetPosition;

            healthObject.transform.DOMove(originalPositions[i], 1f);
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0 || healthObjects.Count == 0)
        {
            Debug.Log("You lost the game");
            lostMenuCanvas.gameObject.SetActive(true);
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
