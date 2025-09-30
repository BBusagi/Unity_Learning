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


    private string storyPath = Constants.STORY_PATH;
    private string defaultStoryFile = Constants.DEFAULT_STORY_FILE_NAME;
    private List<ExcelReader.ExcelData> storyData;
    private int currentLine = 0;



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

        currentLine++;
    }

    private void UpdateAvatarImage(string imageFile)
    {
        string imagePath = Constants.AVATAR_PATH + imageFile;
        Sprite sprite = Resources.Load<Sprite>(imagePath);
        if (sprite != null)
        {
            avatarImage.sprite = sprite;
            avatarImage.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Failed to load image: " + imageFile);
        }
    }

    private void PlayVocalAudio(string audioFile)
    {
        string audioPath = Constants.VOCAL_PATH + audioFile;
        AudioClip audioClip = Resources.Load<AudioClip>(audioPath);
        if (audioClip != null)
        {
            vocalAudio.clip = audioClip;
            vocalAudio.Play();
        }
        else
        {
            Debug.LogError("Failed to load audio:" + audioFile);
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
