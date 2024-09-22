using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    public int scoreAmount = 0; //set to 0 as default
    public Camera mainCamera;
    [SerializeField] private float spaceBetweenObjects;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private GameObject textObject;

    [Header("Score Passive Gain")]
    [SerializeField] private float timerForScoreGain;
    [SerializeField] private int scoreTimerAmount;
    private Vector3 originalPosition; // Store the original positions

    private float timer = 0f;
    private void Start()
    {
        originalPosition = textObject.transform.position;
        UpdateScore();
        PositionObjects();
    }

    private void Update()
    {
        GainPassiveScore();
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
                //  RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
            else
            {
                Debug.Log("Landscape Right");
                //  RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
        }
        else
        {
            if (acceleration.y > 0)
            {
                Debug.Log("Portrait Upside Down");
                //RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
            else
            {
                Debug.Log("Portrait");
                //RepositionUIElements(new Vector3(-30, 0, 0));
                PositionObjects();
            }
        }
    }

    private void PositionObjects()
    {
        float aspectRatio = (float)Screen.width / Screen.height;
        Vector3 bottomLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane + 5f));
        float dynamicSpaceBetweenObjects = spaceBetweenObjects * aspectRatio;

        Vector3 position = new Vector3(
                bottomLeftCorner.x - (dynamicSpaceBetweenObjects * (offsetPosition.x)),
                bottomLeftCorner.y - bottomLeftCorner.y,
                bottomLeftCorner.z - bottomLeftCorner.z
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
        if (timer >= timerForScoreGain)
        {
            timer = 0f;
            scoreAmount += scoreTimerAmount;
            UpdateScore();
        }
    }
}
