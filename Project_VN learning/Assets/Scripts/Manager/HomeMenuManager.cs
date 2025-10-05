using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenuManager : MonoBehaviour
{
    public static HomeMenuManager Instance { get; private set; }
    public GameObject HomeMenuPanel;

    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private bool hasStarted = false;
    
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    void Start()
    {
        startButton.onClick.AddListener(OnStartClick);
        continueButton.onClick.AddListener(OnLoadClick);
    }

    private void OnLoadClick()
    {
        if (hasStarted)
        {
            HomeMenuPanel.SetActive(false);
            VNManager.Instance.gamePanel.SetActive(true);
        }
    }

    private void OnStartClick()
    {
        hasStarted = true;
        VNManager.Instance.StartGame();
        HomeMenuPanel.SetActive(false);
        VNManager.Instance.gamePanel.SetActive(true);
    }

    void Update()
    {

    }
}
