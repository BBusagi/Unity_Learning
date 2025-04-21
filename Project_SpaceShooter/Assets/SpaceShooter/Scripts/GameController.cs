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

    [Header("GameOver Components")]
    [SerializeField] private GameObject GameOverScreen;

    public enum GameState
    {
        Waiting,
        Playing,
        GameOver
    }

    private static GameState _currentGameStatus;
    public static GameState CurrentGameStatus
    {
        get => _currentGameStatus;
        set
        {
            if (_currentGameStatus != value)
            {
                _currentGameStatus = value;
                Debug.Log($"[GameState] changed to: {_currentGameStatus}");
            }
        }
    }

    protected void Awake()
    {
        _currentGameStatus = GameState.Waiting;
    }

    void Start()
    {

    }


    void Update()
    {
        if (_currentGameStatus == GameState.Playing) AdjustTimer();
    }

    private void AdjustTimer()
    {
        timerImage.fillAmount = sliderCurrentFillAmount - Time.deltaTime / gameTime;

        sliderCurrentFillAmount = timerImage.fillAmount;

        if (sliderCurrentFillAmount <= 0)
        {
            GameOver();
        }
    }

    public void UpdateGameScore(int asteroidScore)
    {
        if (_currentGameStatus != GameState.Playing) return;

        playerScore += asteroidScore;
        scoreText.text = playerScore.ToString();
    }

    public void StartGame()
    {
        _currentGameStatus = GameState.Playing;
    }

    public void GameOver()
    {
        _currentGameStatus = GameState.GameOver;

        GameOverScreen.SetActive(true);
    }

    public void ResetGame()
    {
        _currentGameStatus = GameState.Waiting;

        sliderCurrentFillAmount = 1;
        timerImage.fillAmount = 1;

        playerScore = 0;
        scoreText.text =  0.ToString();
    }
}

