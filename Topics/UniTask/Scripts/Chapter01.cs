using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Chapter01 : MonoBehaviour
{
    public TMP_Text TextBoard;
    public Button LoadTextButton;
    public Button LoadSceneButton;
    public Slider slider;
    public Button WebRequestButton;
    public Image DownloadImage;

    void Start()
    {
        LoadTextButton.onClick.AddListener(OnLoadText);
        LoadSceneButton.onClick.AddListener(OnLoadScene);
        WebRequestButton.onClick.AddListener(OnLoadImageWebRequest);
    }

    private async void OnLoadScene()
    {
        await SceneManager.LoadSceneAsync("Scenes/TargetScene").ToUniTask(
            (Progress.Create<float>(
                (p) =>
                {
                    Debug.Log("OnLoadScene Progress: " + p);
                    slider.value = p;
                }
            ))
        );
    }

    private async void OnLoadText()
    {
        // ResourceRequest loadOperation = Resources.LoadAsync<TextAsset>("Test"); //Async
        // var text = await loadOperation; //await => ResourceRequestAwaiter, GetResult(), asyncOperation.asset
        // TextBoard.text = ((TextAsset) text).text;

        UniTaskTools unitaskAsyncLoader = new UniTaskTools();
        TextBoard.text = ((TextAsset)(await unitaskAsyncLoader.LoadAsync<TextAsset>("Test"))).text;
    }

    private async void OnLoadImageWebRequest()
    {
        var webRequest = UnityWebRequestTexture.GetTexture("https://i0.hdslb.com/bfs/static/jinkela/video/asserts/33-coin-ani.png");
        var result = await webRequest.SendWebRequest();
        var texture = ((DownloadHandlerTexture)result.downloadHandler).texture;

        int spriteCount = 24;
        int perSperiteWidth = texture.width / spriteCount;
        Sprite[] sprites = new Sprite[spriteCount];
        for (int i = 0; i < spriteCount; i++)
        {
            sprites[i] = Sprite.Create(
                texture,
                new Rect(perSperiteWidth * i, 0, perSperiteWidth, texture.height),
                new Vector2(0.5f, 0.5f)
                );
        }
        float perFrameTime = 0.1f;
        while (true)
        {
            for (int i = 0; i < spriteCount; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(perFrameTime));
                var sprite = sprites[i];
                DownloadImage.sprite = sprite;
            }
        }
    }
}
