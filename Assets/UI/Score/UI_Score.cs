using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    public int scoreAmount = 0;
    public Camera mainCamera;
    [SerializeField] private float spaceBetweenObjects;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private GameObject textObject;

    [Header("Score Passive Gain")]
    [SerializeField] private float timerForScoreGain;
    [SerializeField] private int scoreTimerAmount;

    private Vector3 originalPosition;
    private float timer = 0f;

    private void Start()
    {
        originalPosition = textObject.transform.position;
        UpdateScore();
        PositionObjects();

        if (mainCamera == null)
        {
            StartCoroutine(GetCameraWithDelay());
        }
    }

    private void Update()
    {
        GainPassiveScore();
        CheckForRotations();
    }

    private IEnumerator GetCameraWithDelay()
    {
        yield return new WaitForSeconds(0.1f);

        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            mainCamera = FindAnyObjectByType<Camera>();
        }

        if (mainCamera == null)
        {
            Debug.LogWarning("Main Camera could not be found. Ensure a Camera is tagged as 'MainCamera' or present in the scene.");
        }
    }

    private void CheckForRotations()
    {
        Vector3 acceleration = Input.acceleration;

        if (Mathf.Abs(acceleration.x) > Mathf.Abs(acceleration.y))
        {
            PositionObjects();
        }
        else
        {
            PositionObjects();
        }
    }

    private void PositionObjects()
    {
        if (mainCamera == null) return; // Exit if camera is still null

        float aspectRatio = (float)Screen.width / Screen.height;
        Vector3 bottomLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane + 5f));
        float dynamicSpaceBetweenObjects = spaceBetweenObjects * aspectRatio;

        Vector3 position = new Vector3(
            bottomLeftCorner.x - (dynamicSpaceBetweenObjects * (offsetPosition.x)),
            bottomLeftCorner.y,
            bottomLeftCorner.z
        );

        textObject.transform.position += position + new Vector3(-3000, 6000, 200);
        textObject.transform.SetParent(gameObject.transform, true);

        textObject.transform.DOMove(position, 0.5f);
    }

    public void GainScore(int scoreAdded)
    {
        scoreAmount += scoreAdded;
        UpdateScore();
    }

    private void UpdateScore()
    {
        score.text = "Score: " + scoreAmount;
    }

    private void GainPassiveScore()
    {
        if (timer < timerForScoreGain)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            scoreAmount += scoreTimerAmount;
            UpdateScore();
        }
    }
}
