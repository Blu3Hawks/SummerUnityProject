using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    public int scoreAmount = 0; //set to 0 as default

    [Header("Score Passive Gain")]
    [SerializeField] private float timerForScoreGain;
    [SerializeField] private int scoreTimerAmount;

    private float timer = 0f;
    private void Start()
    {
        UpdateScore();
    }

    private void Update()
    {
        GainPassiveScore();
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
