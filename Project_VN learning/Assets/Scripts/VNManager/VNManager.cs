using System;
using System.Collections;
using System.Collections.Generic;
using ExcelDataReader;
using TMPro;
using Unity.Loading;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// 视觉小说总体控制�?
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


    private string storyPath = Constants.STORY_PATH;
    private string defaultStoryFile = Constants.DEFAULT_STORY_FILE_NAME;
    private List<ExcelReader.ExcelData> storyData;
    private int currentLine = Constants.DEFAULT_START_LINE;



    void Start()
    {
        Initialize();
        LoadStoryFromFile(storyPath + defaultStoryFile);
        DisplayNextLine();
    }

    void Update()
    {
        // 用户输入 鼠标
        if (Input.GetMouseButtonDown(0))
        {
            DisplayNextLine();
        }
    }



    private void Initialize()
    {
        avatarImage.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
        character1Image.gameObject.SetActive(false);
        character2Image.gameObject.SetActive(false);
    }

    private void LoadStoryFromFile(string path)
    {
        Debug.Log("[Debug] Start to read file: " + path);
        storyData = ExcelReader.ReadExcel(path);
        if (storyData == null || storyData.Count == 0)
        {
            Debug.LogError("No data found in file");
        }
    }

    private void DisplayNextLine()
    {
        if (currentLine >= storyData.Count)
        {
            Debug.Log("[Debug] End of story");
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

    private void DisplayThisLine()
    {
        var data = storyData[currentLine];
        speakerName.text = data.speaker;
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
            UpdateCharacterImage(data.charater1Action, data.charater1ImageFile, character1Image, data.CoordinateX1);
        }
        if (NotNullNorEmpty(data.charater2Action))
        {
            UpdateCharacterImage(data.charater2Action, data.charater2ImageFile, character2Image, data.CoordinateX2);
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



    private bool NotNullNorEmpty(string str)
    {
        return !string.IsNullOrEmpty(str);
    }
}

