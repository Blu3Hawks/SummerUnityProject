using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HealthHandler : MonoBehaviour
{
    // for every health amount, have this item prefab show and rotate
    // health amount - int
    // max and min health - maybe it's better to have a list of these health items, enable and disable them accordingly
    // so let's go with that. when adding health, enable another health, if max health already then don't add, if removed then disable one object
    //after removing always check if it's more than 0 - if less then game lost, if max health don't add
    public Camera mainCamera;

    [Header("Health")]
    public GameObject[] healthObjects;
    public int currentHealth;
    [SerializeField] private int maxHealth = 3; // default values


    [Header("Rotation & Position")]
    [SerializeField] private int rotationSpeed;
    [SerializeField] private float spaceBetweenObjects;

    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Transform healthObjectParent;
    ScreenOrientation lastOrientation;


    private void Start()
    {
        currentHealth = maxHealth;
        PositionObjects();
        lastOrientation = Screen.orientation;
    }


    private void Update()
    {
        RotateHealth();
        UISmoothRotation();
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
        }
    }

    private void UISmoothRotation()
    {
        ScreenOrientation currentOrientation = Screen.orientation;

        if (lastOrientation != currentOrientation)
        {
            lastOrientation = currentOrientation;
            RepositionUIElements();
        }
    }

    private void RepositionUIElements()
    {
        foreach(GameObject healthObject in healthObjects)
        {
            Vector3 originalPos = healthObject.transform.position;
            healthObject.transform.position += new Vector3(30, 0, 0);
            healthObject.transform.position = Vector3.Slerp(originalPos, healthObject.transform.position, 0.5f);
        }
    }


    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth < 0)
        {
            Debug.Log("You lost the game"); // restart the game
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
