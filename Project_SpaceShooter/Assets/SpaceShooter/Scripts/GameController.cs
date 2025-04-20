using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoSingleton<GameController>
{
    public void Test() => Debug.Log("Hello from Singleton");

    [SerializeField] private Image timerImage;
    [SerializeField] private float gameTime;

    private float sliderCurrentFillAmount = 1f;
    private int playerScore;

    [Header("Score Components")]
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        
    }


    void Update()
    {
        AdjustTimer();
    }

    private void AdjustTimer()
    {
        timerImage.fillAmount = sliderCurrentFillAmount - Time.deltaTime / gameTime;

        sliderCurrentFillAmount = timerImage.fillAmount;
    }

    public void UpdateGameScore(int asteroidScore)
    {
        playerScore += asteroidScore;
        scoreText.text = playerScore.ToString();
    }
}

