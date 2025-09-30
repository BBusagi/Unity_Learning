using System;
using System.Collections;
using System.Collections.Generic;
using ExcelDataReader;
using TMPro;
using Unity.Loading;
using UnityEngine;


/// <summary>
/// 视觉小说总体控制器
/// </summary>
public class VNManager : MonoBehaviour
{
    public TypeWriterEffect typeWriterEffect;

    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private TextMeshProUGUI speakerContent;

    private string filePath = Constants.STORY_PATH;
    private List<ExcelReader.ExcelData> storyData;
    private int currentLine = 0;



    void Start()
    {
        LoadStoryFromFile(filePath);
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
            var data = storyData[currentLine];
            speakerName.text = data.speaker;
            typeWriterEffect.StartTyping(data.content);
            currentLine++;
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
}
