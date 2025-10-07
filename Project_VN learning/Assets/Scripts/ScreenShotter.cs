using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotter : MonoBehaviour
{
    public Texture2D CaptureScreenShot()
    {
        //获取屏幕尺寸
        int width = Screen.width;
        int height = Screen.height;
        //创建渲染纹理（临时）
        RenderTexture rt = RenderTexture.GetTemporary(width, height, 24);
        //设置摄像机渲染目标
        Camera mainCamera = Camera.main;
        mainCamera.targetTexture = rt;
        RenderTexture.active = rt;
        mainCamera.Render();
        //读取像素数值并存储
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();
        //释放资源并重置
        mainCamera.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        //制作缩略图 删除原始截图
        Texture2D resizedScreenshot = ResizeTexture(screenshot, width / 6, height / 6);
        Destroy(screenshot);

        return resizedScreenshot;
    }

    private Texture2D ResizeTexture(Texture2D origin, int newWidth, int newHeight)
    {
        //创建渲染纹理
        RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight, 24);
        RenderTexture.active = rt;
        //GPU进行缩放
        Graphics.Blit(origin, rt);
        //存储新数据
        Texture2D resized = new Texture2D(newWidth, newHeight, TextureFormat.RGB24, false);
        resized.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
        resized.Apply();
        //释放资源
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        return resized;
    }
}
