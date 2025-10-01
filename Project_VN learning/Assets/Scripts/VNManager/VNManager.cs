using System;
using System.Collections;
using System.Collections.Generic;
using ExcelDataReader;
using TMPro;
using Unity.Loading;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 视觉小说总体控制器
/// </summary>
public class VNManager : MonoBehaviour
{
    public TypeWriterEffect typeWriterEffect;

    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private TextMeshProUGUI speakerContent;
    [SerializeField] private Image avatarImage;
    [SerializeField] private AudioSource vocalAudio;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private Image character1Image;
    [SerializeField] private Image character2Image;


    private string storyPath = Constants.STORY_PATH;
    private string defaultStoryFile = Constants.DEFAULT_STORY_FILE_NAME;
    private List<ExcelReader.ExcelData> storyData;
    private int currentLine = Constants.DEFAULT_START_LINE;



    void Start()
    {
        LoadStoryFromFile(storyPath + defaultStoryFile);
        DisplayNextLine();
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
            UpdateCharacterImage(data.charater1Action, data.charater1ImageFile, character1Image);
        }
        if (NotNullNorEmpty(data.charater2Action))
        {
            UpdateCharacterImage(data.charater2Action, data.charater2ImageFile, character2Image);
        }

        currentLine++;
    }

    private void UpdateCharacterImage(string action, string imageFile, Image characterImage)
    {
        if (action.StartsWith(Constants.charaterActionAppearAt))
        {
            string imagePath = Constants.CHARACTER_PATH + imageFile;
            UpdateImage(imagePath, characterImage);
        }
        else if (action.StartsWith(Constants.charaterActionMoveTo))
        { }
        else if (action == Constants.charaterActionDisappear)
        {
            characterImage.gameObject.SetActive(false); //TODO 添加动画消失效果
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
        string audioPath = Constants.BGM_PATH + audioFile;
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
            if (audioSource == vocalAudio)
            { 
                Debug.LogError("Failed to load audio: " + audioPath);
            }
            else if (audioSource == backgroundAudio)
            {
                Debug.LogError("Failed to load BGM: " + audioPath);
            }
        }
    }


    void Update()
    {
        // 用户输入 鼠标
        if (Input.GetMouseButtonDown(0))
        {
            DisplayNextLine();
        }
    }

    private bool NotNullNorEmpty(string str)
    {
        return !string.IsNullOrEmpty(str);
    }
}
