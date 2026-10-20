using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Chapter02_1 : MonoBehaviour
{
    public Button TestButton;
    public string SearchWords = "Unity";

    public string[] SearchURLs = 
    {
        "https://www.baidu.com/s?wd=",
        "https://www.bing.com/",
        "https://www.google.co.jp/"
    };

    void Start()
    {
        TestButton.onClick.AddListener(UniTask.UnityAction(OnClickTest));
    }

    private async UniTask<string> GetRequest(string url, float timeout)
    {
        var cts = new CancellationTokenSource();
        cts.CancelAfterSlim(TimeSpan.FromSeconds(timeout)); //5s timeout

        var (cancelOrFailed, result) = await UnityWebRequest.Get(url).SendWebRequest().WithCancellation(cts.Token).SuppressCancellationThrow();
        if(!cancelOrFailed)
        {
            return result.downloadHandler.text.Substring(0,100);
        }
        return "Timeout!";
    }

    private async UniTaskVoid OnClickTest()
    {
        UniTask<string>[] waitTasks = new UniTask<string>[SearchURLs.Length];

        for(int i = 0; i < SearchURLs.Length; i++)
        {
            waitTasks[i] = GetRequest(SearchURLs[i], 2f);
        }

        var tasks = await UniTask.WhenAll(waitTasks);
        for(int i = 0; i<tasks.Length; i++)
        {
            Debug.Log(tasks[i]);
        }
    }
}
