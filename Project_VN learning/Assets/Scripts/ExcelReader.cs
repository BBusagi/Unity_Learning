using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExcelDataReader;
using UnityEngine;

/// <summary>
/// 读取Excel内的信息
/// </summary>
public class ExcelReader
{
    public struct ExcelData
    {
        public string speaker;
        public string content;
        public string avatarImageFile;
        public string vocalAudioFile;
        public string backgroundImageFile;
        public string backgroundMusicFile;
        public string charater1Action;
        public string charater1ImageFile;
        public string charater2Action;
        public string charater2ImageFile;

    }

    public static List<ExcelData> ReadExcel(string filePath)
    {
        List<ExcelData> excelData = new List<ExcelData>();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    while (reader.Read())
                    {
                        ExcelData data = new ExcelData();
                        data.speaker = reader.IsDBNull(0) ? string.Empty : reader.GetValue(0)?.ToString();
                        data.content = reader.IsDBNull(1) ? string.Empty : reader.GetValue(1)?.ToString();
                        data.avatarImageFile = reader.IsDBNull(2) ? string.Empty : reader.GetValue(2)?.ToString();
                        data.vocalAudioFile = reader.IsDBNull(3) ? string.Empty : reader.GetValue(3)?.ToString();
                        data.backgroundImageFile = reader.IsDBNull(4) ? string.Empty : reader.GetValue(4)?.ToString();
                        data.backgroundMusicFile = reader.IsDBNull(5) ? string.Empty : reader.GetValue(5)?.ToString();
                        //data.CharaterNum
                        data.charater1Action = reader.IsDBNull(6) ? string.Empty : reader.GetValue(6)?.ToString();
                        data.charater1ImageFile = reader.IsDBNull(7) ? string.Empty : reader.GetValue(7)?.ToString();
                        data.charater2Action = reader.IsDBNull(8) ? string.Empty : reader.GetValue(8)?.ToString();
                        data.charater2ImageFile = reader.IsDBNull(9) ? string.Empty : reader.GetValue(9)?.ToString();

                        excelData.Add(data);
                    }
                }
                while (reader.NextResult());
            }
        }
        return excelData;
    }

}
