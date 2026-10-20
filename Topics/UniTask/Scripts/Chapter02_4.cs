using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Chapter02_4 : MonoBehaviour
{
    public Button StandardRun;
    public Button YieldRun;
    void Start()
    {
        StandardRun.onClick.AddListener(UniTask.UnityAction(OnClickStandardRun));
        YieldRun.onClick.AddListener(UniTask.UnityAction(OnClickYieldRun));
    }
    async UniTaskVoid OnClickStandardRun()
    {
        int result = 0;
        await UniTask.RunOnThreadPool(() => { result = 1; });
        await UniTask.SwitchToMainThread();
        Debug.Log(result);
    }

    async UniTaskVoid OnClickYieldRun()
    {
        string fileName = Application.dataPath + "/Resources/test.txt";
        await UniTask.SwitchToThreadPool();
        string fileContent = await File.ReadAllTextAsync(fileName);
        await UniTask.Yield(PlayerLoopTiming.Update);
        Debug.Log(fileContent);
    }
}
