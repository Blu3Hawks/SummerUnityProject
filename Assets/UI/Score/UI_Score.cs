using DG.Tweening;
using System.Collections;
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
    private bool isPositioned = false;

    private void Start()
    {
        originalPosition = textObject.transform.position;
        UpdateScore();
        PositionObjects();
        mainCamera = FindAnyObjectByType<Camera>();
        if( mainCamera == null )
        {
            StartCoroutine(GetCameraWithDelay());
        }
    }

    private void Update()
    {
        GainPassiveScore();
        if (!isPositioned)
        {
            CheckForRotations();
        }
    }

    private IEnumerator GetCameraWithDelay()
    {
        yield return new WaitForSeconds(0.1f);

        Debug.Log("We try to find the camera");
        if (mainCamera == null)
        {
            mainCamera = FindAnyObjectByType<Camera>();
            if (mainCamera == null)
            {
                Debug.LogWarning("Main Camera could not be found. Ensure a Camera is tagged as 'MainCamera' or present in the scene.");
                StartCoroutine(GetCameraWithDelay());
            }
        }
    }

    private void CheckForRotations()
    {
        PositionObjects();
        isPositioned = true; // Set to true after positioning
    }

    private void PositionObjects()
    {
        if (mainCamera == null) return;

        float aspectRatio = (float)Screen.width / Screen.height;
        Vector3 bottomLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane + 5f));
        float dynamicSpaceBetweenObjects = spaceBetweenObjects * aspectRatio;

        Vector3 position = new Vector3(
            bottomLeftCorner.x - (dynamicSpaceBetweenObjects * offsetPosition.x),
            bottomLeftCorner.y,
            bottomLeftCorner.z
        );

        textObject.transform.position = originalPosition; // Reset to original before applying final move
        textObject.transform.DOMove(position, 0.5f).SetUpdate(true);
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

    public void AddScore(int gainedScore)
    {
        scoreAmount += gainedScore;
    }

    private void OnDisable()
    {
        if (PlayerProfile.Instance != null && PlayerProfile.Instance.currentPlayerData != null)
        {
            PlayerProfile.Instance.currentPlayerData.totalScore += scoreAmount;
            PlayerProfile.Instance.SavePlayerData();
        }
    }

    private void OnEnable()
    {
        // Reset position when re-enabling
        textObject.transform.position = originalPosition;
        UpdateScore();
        isPositioned = false; // Reset to allow positioning
    }
}
