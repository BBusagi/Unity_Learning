using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
/// <summary>
/// 该脚本不继承monobehavriour，即可在非Unity托管中运行
/// </summary>
public class UniTaskTools
{
    public async UniTask<Object> LoadAsync<T>(string path) where T : Object
    {
        ResourceRequest loadOperation = Resources.LoadAsync<T>(path);
        return await loadOperation;
    }

    public async UniTask<int> WaitYield(PlayerLoopTiming loopTiming)
    {
        await UniTask.Yield(loopTiming);
        return 0;
    }

    public async UniTask<int> WaitNextFrame()
    {
        await UniTask.NextFrame();
        return Time.frameCount;
    }

    public async UniTask<int> WaitEndofFrame (MonoBehaviour behaviour)
    {
        await UniTask.WaitForEndOfFrame(behaviour);
        return Time.frameCount;
    }
}
