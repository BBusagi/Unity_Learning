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

    [Header("Player Score Components")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private int playerScore;

    
    [Header("High Score Components")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    private int highScore;


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

        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }

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

        if(playerScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            highScoreText.text = playerScore.ToString();
        }
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

