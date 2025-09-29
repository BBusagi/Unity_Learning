using System;
using System.Collections;
using System.Collections.Generic;
using ExcelDataReader;
using Unity.Loading;
using UnityEngine;



public class VNManager : MonoBehaviour
{
    private string filePath = Constants.STORY_PATH;
    void Start()
    {
        LoadStoryFromFile(filePath);
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        throw new NotImplementedException();
    }

    private void LoadStoryFromFile(string filePath)
    {
        //storyData = ExcelDataReader.Load(filePath);
        throw new NotImplementedException();
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
