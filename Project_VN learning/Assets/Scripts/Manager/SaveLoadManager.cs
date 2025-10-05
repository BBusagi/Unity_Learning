using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private GameObject saveLoadPanel;
    [SerializeField]
    private TextMeshProUGUI titlePanel
    ;
    [SerializeField] private Button[] saveLoadButtons;
    [SerializeField] private Button prevPageButton;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button returnButton;

    private bool isSave;
    private int currentPage = Constants.DEFAULT_START_INDEX;
    private readonly int slotsPerPage = Constants.SLOTS_PER_PAGE;
    private readonly int totalSlots = Constants.TOTAL_SLOTS;

    public static SaveLoadManager Instance { get; private set; }
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
        prevPageButton.onClick.AddListener(PrevPageClick);
        nextPageButton.onClick.AddListener(NextPageClick);
        returnButton.onClick.AddListener(OnReturnButtonClick);
        saveLoadPanel.SetActive(false);
    }

    void Update()
    {

    }



    public void ShowSaveLoadUI(bool save)
    {
        isSave = save;
        titlePanel.text = save ? Constants.SAVE_GAME : Constants.LOAD_GAME;
        UpdateSaveLoadUI();
        saveLoadPanel.SetActive(true);
        LoadStorylineAndScreenshots();
    }

    private void UpdateSaveLoadUI()
    {
        for (int i = 0; i < slotsPerPage; i++)
        {
            int slotIndex = currentPage * slotsPerPage + i;
            if (slotIndex < totalSlots)
            {
                saveLoadButtons[i].gameObject.SetActive(true);
                saveLoadButtons[i].interactable = true;

                var slotText = (slotIndex + 1) + Constants.COLON + Constants.EMPTY_SLOT;
                var textComponents = saveLoadButtons[i].gameObject.GetComponentsInChildren<TextMeshProUGUI>();
                textComponents[0].text = null;
                textComponents[1].text = slotText;
                saveLoadButtons[i].GetComponentInChildren<RawImage>().texture = null;
            }
            else
            {
                saveLoadButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void LoadStorylineAndScreenshots()
    {
        return;
    }

    private void PrevPageClick()
    {
                if (currentPage > 0)
        {
            currentPage--;
            UpdateSaveLoadUI();
            LoadStorylineAndScreenshots();
        }
    }

    private void NextPageClick()
    {
        if ((currentPage + 1) * slotsPerPage < totalSlots)
        {
            currentPage++;
            UpdateSaveLoadUI();
            LoadStorylineAndScreenshots();
        }
    }

    private void OnReturnButtonClick()
    {
        saveLoadPanel.SetActive(false);
    }
}
