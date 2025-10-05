using System;
using System.Collections;
using System.Collections.Generic;
using ExcelDataReader;
using TMPro;
using Unity.Loading;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;


/// <summary>
/// 视觉小说总体控制
/// </summary>
public class VNManager : MonoBehaviour
{
    public TypeWriterEffect typeWriterEffect;

    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private TextMeshProUGUI speakerContent;

    [SerializeField] private AudioSource vocalAudio;
    [SerializeField] private AudioSource backgroundAudio;

    [SerializeField] private Image avatarImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image character1Image;
    [SerializeField] private Image character2Image;

    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button choiceButton1;
    [SerializeField] private Button choiceButton2;

    // button panel
    [SerializeField] private GameObject buttonButton;
    [SerializeField] private Button autoButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;

    private readonly string storyPath = Constants.STORY_PATH;
    private readonly string defaultStoryFile = Constants.DEFAULT_STORY_FILE_NAME;
    private readonly string excelExtension = Constants.EXCEL_FILE_EXTENSION;
    private List<ExcelReader.ExcelData> storyData;
    private int currentLine;
    private string currentStoryFile;

    private bool isAutoPlay = false;
    private bool isSkip = false;
    private int maxRearchedLine = 0;
    private Dictionary<string, int> globalMaxRearchedLineDic = new Dictionary<string, int>();




    void Start()
    {
        ButtonAddListener();
        InitializeAndLoadStory(defaultStoryFile);
    }

    private void ButtonAddListener()
    {
        autoButton.onClick.AddListener(OnAutoButtonClick);
        skipButton.onClick.AddListener(OnSkipButtonClick);
        saveButton.onClick.AddListener(OnSaveButtonClick);
        loadButton.onClick.AddListener(OnLoadButtonClick);
    }

    private void OnLoadButtonClick()
    {
        SaveLoadManager.Instance.ShowSaveLoadUI(false);
    }

    private void OnSaveButtonClick()
    {
        SaveLoadManager.Instance.ShowSaveLoadUI(true);
    }

    private void OnSkipButtonClick()
    {
        if (!isSkip && CanSkip())
        {
            StartSkip();
        }
        else if (isSkip)
        {
            StopCoroutine(SkipToMaxReachedLine());
            EndSkip();
        }
    }

    private void EndSkip()
    {
        isSkip = false;
        typeWriterEffect.TypingSpeed = Constants.DEFAULT_TYPING_SPEED;
        UpdateButtonImage(Constants.SKIP_OFF, skipButton);
    }

    private void StartSkip()
    {
        isSkip = true;
        UpdateButtonImage(Constants.SKIP_ON, skipButton);
        typeWriterEffect.TypingSpeed = Constants.FAST_TYPING_SPEED;
        StartCoroutine(SkipToMaxReachedLine());
    }

    private bool CanSkip()
    {
        return currentLine < maxRearchedLine;
    }

    void Update()
    {
        // 用户输入 鼠标
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsHittingButtons()) DisplayNextLine();
        }
    }

    private void InitializeAndLoadStory(string fileName)
    {
        Initialize();
        LoadStoryFromFile(fileName);
        DisplayNextLine();
    }

    private void Initialize()
    {
        currentLine = Constants.DEFAULT_START_LINE;
        avatarImage.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
        character1Image.gameObject.SetActive(false);
        character2Image.gameObject.SetActive(false);
        choicePanel.SetActive(false);
    }

    private void LoadStoryFromFile(string fileName)
    {
        Debug.Log("[Debug] Start to read file: " + fileName);

        currentStoryFile = fileName;
        var path = storyPath + fileName + excelExtension;
        storyData = ExcelReader.ReadExcel(path);
        if (storyData == null || storyData.Count == 0) Debug.LogError("No data found in file");

        if (globalMaxRearchedLineDic.ContainsKey(currentStoryFile))
        {
            maxRearchedLine = globalMaxRearchedLineDic[currentStoryFile];
        }
        else
        {
            maxRearchedLine = 0;
            globalMaxRearchedLineDic[currentStoryFile] = maxRearchedLine;
        }
    }



    private void DisplayNextLine()
    {
        if (currentLine > maxRearchedLine)
        {
            maxRearchedLine = currentLine;
            globalMaxRearchedLineDic[currentStoryFile] = maxRearchedLine;
        }
        if (currentLine >= storyData.Count - 1)
        {
            if (isAutoPlay)
            {
                isAutoPlay = false;
                UpdateButtonImage(Constants.AUTO_OFF, autoButton);
            }
            if (storyData[currentLine].speakerName == Constants.STORYCONTROL_End)
            {
                Debug.Log("[Debug] End of story");
            }
            if (storyData[currentLine].speakerName == Constants.STORYCONTROL_CHOICE)
            {
                ShowChoice();
            }
            return;
        }

        if (typeWriterEffect.IsTyping)
        {
            typeWriterEffect.completeTyping();
        }
        else
        {
            DisplayThisLine();
        }
    }
    private void ShowChoice()
    {
        choiceButton1.onClick.RemoveAllListeners();
        choiceButton2.onClick.RemoveAllListeners();
        choicePanel.SetActive(true);


        var data = storyData[currentLine];
        choiceButton1.GetComponentInChildren<TextMeshProUGUI>().text = data.content; //第二列
        choiceButton1.onClick.AddListener(() => InitializeAndLoadStory(data.avatarImageFile)); //第三列
        choiceButton2.GetComponentInChildren<TextMeshProUGUI>().text = data.vocalAudioFile; //第四列
        choiceButton2.onClick.AddListener(() => InitializeAndLoadStory(data.backgroundImageFile)); //第五列
    }

    private void DisplayThisLine()
    {
        var data = storyData[currentLine];
        speakerName.text = data.speakerName;
        typeWriterEffect.StartTyping(data.content);

        //dialogue
        if (NotNullNorEmpty(data.avatarImageFile))
        {
            UpdateAvatarImage(data.avatarImageFile);
        }
        else
        {
            avatarImage.gameObject.SetActive(false);
        }
        if (NotNullNorEmpty(data.vocalAudioFile))
        {
            PlayVocalAudio(data.vocalAudioFile);
        }

        //background
        if (NotNullNorEmpty(data.backgroundImageFile))
        {
            UpdateBackgroundImage(data.backgroundImageFile);
        }
        if (NotNullNorEmpty(data.backgroundMusicFile))
        {
            PlayVBackgroundAudio(data.backgroundMusicFile);
        }

        // charaterAction
        if (NotNullNorEmpty(data.charater1Action))
        {
            UpdateCharacterImage(data.charater1Action, data.charater1ImageFile, character1Image, data.coordinateX1);
        }
        if (NotNullNorEmpty(data.charater2Action))
        {
            UpdateCharacterImage(data.charater2Action, data.charater2ImageFile, character2Image, data.coordinateX2);
        }

        currentLine++;
    }

    private void UpdateCharacterImage(string action, string imageFile, Image characterImage, string x)
    {
        if (action.StartsWith(Constants.charaterActionAppearAt)) //appearAt
        {
            string imagePath = Constants.CHARACTER_PATH + imageFile;
            if (NotNullNorEmpty(x))
            {
                UpdateImage(imagePath, characterImage);
                var newPosition = new Vector2(float.Parse(x), characterImage.rectTransform.anchoredPosition.y);
                characterImage.rectTransform.anchoredPosition = newPosition;
                characterImage.DOFade(1, Constants.DURATION_TIME).From(0);
            }
            else
            {
                Debug.LogError("Coordinate missing");
            }
        }
        else if (action.StartsWith(Constants.charaterActionMoveTo)) //moveTo
        {
            if (NotNullNorEmpty(x))
            {
                characterImage.rectTransform.DOAnchorPosX(float.Parse(x), Constants.DURATION_TIME);
            }
            else
            {
                Debug.LogError("Coordinate missing");
            }
        }
        else if (action == Constants.charaterActionDisappear) //disappear
        {
            characterImage.DOFade(0f, Constants.DURATION_TIME).OnComplete(() => characterImage.gameObject.SetActive(false));
        }
    }

    private void UpdateAvatarImage(string imageFile)
    {
        var imagePath = Constants.AVATAR_PATH + imageFile;
        UpdateImage(imagePath, avatarImage);
    }

    private void PlayVocalAudio(string audioFile)
    {
        string audioPath = Constants.VOCAL_PATH + audioFile;
        PlayAudio(audioPath, vocalAudio);
    }

    private void UpdateBackgroundImage(string imageFile)
    {
        string imagePath = Constants.BACKGROUND_PATH + imageFile;
        UpdateImage(imagePath, backgroundImage);
    }
    private void PlayVBackgroundAudio(string audioFile)
    {
        string audioPath = Constants.MUSIC_PATH + audioFile;
        PlayAudio(audioPath, backgroundAudio, true);
    }

    private void UpdateImage(string imagePath, Image image)
    {
        Sprite sprite = Resources.Load<Sprite>(imagePath);
        if (sprite != null)
        {
            image.sprite = sprite;
            image.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Failed to load image: " + imagePath);
        }
    }

    private void PlayAudio(string audioPath, AudioSource audioSource, bool isLoop = false)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(audioPath);
        if (audioClip != null)
        {
            backgroundAudio.clip = audioClip;
            backgroundAudio.Play();
            backgroundAudio.loop = isLoop;
        }
        else
        {
            Debug.LogError("Failed to load audio: " + audioPath);
        }
    }

    private void OnAutoButtonClick()
    {
        isAutoPlay = !isAutoPlay;
        Debug.Log("[Debug] OnAutoButtonClick" + isAutoPlay);

        UpdateButtonImage((isAutoPlay ? Constants.AUTO_ON : Constants.AUTO_OFF), autoButton);
        if (isAutoPlay)
        {
            StartCoroutine(StartAutoPlay());
        }
    }

    private void UpdateButtonImage(string imageFile, Button button)
    {
        string imagePath = Constants.BUTTON_PATH + imageFile;
        UpdateImage(imagePath, button.image);
    }

    private IEnumerator StartAutoPlay()
    {
        while (isAutoPlay)
        {
            if (!typeWriterEffect.IsTyping) DisplayNextLine();

            yield return new WaitForSeconds(Constants.AUTO_WAITING_SECONDS);
        }
    }

    private IEnumerator SkipToMaxReachedLine()
    {
        while (isSkip)
        {
            if (CanSkip())
            {
                DisplayThisLine();
            }
            else
            {
                EndSkip();
            }
            yield return new WaitForSeconds(Constants.SKIP_WAITING_SECONDS);
        }
    }


    #region Utility

    private bool IsHittingButtons()
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            buttonButton.GetComponent<RectTransform>(),
            Input.mousePosition,
            null
        );
    }

    private bool NotNullNorEmpty(string str)
    {
        return !string.IsNullOrEmpty(str);
    }

    #endregion

}

