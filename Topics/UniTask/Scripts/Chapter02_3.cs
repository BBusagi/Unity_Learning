using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Chapter02_3 : MonoBehaviour
{
    public Button button;
    public GameObject targetGO;
    public const float G = 9.8f;
    public float FallTime = 3f;

    void Start()
    {
        button.onClick.AddListener(UniTask.UnityAction(OnClickCallBack));
    }

    private async UniTaskVoid FallTarget(Transform targetTrans, float falltime, System.Action onHalf, UniTaskCompletionSource source)
    {
        float startTime = Time.time;
        Vector3 startPos = targetTrans.position;
        float lastElapsedTime = 0;
        while (Time.time - startTime <= falltime)
        {
            float elapsedTime = Mathf.Min(Time.time - startTime, falltime);
            if (lastElapsedTime < falltime * 0.5f && elapsedTime >= falltime * 0.5f)
            {
                onHalf?.Invoke();
                source.TrySetResult(); // 手动完成
                //source.TrySetException(new SystemException());  失败 
                //source.TrySetCanceled(someToken);  取消 
                //tryset之后的返回值为任务状态（结束与否），而不是任务结果
            }

            lastElapsedTime = elapsedTime;
            float fallY = 0 + 0.5f * G * elapsedTime * elapsedTime;
            targetTrans.position = startPos + Vector3.down * fallY;
            await UniTask.Yield(this.GetCancellationTokenOnDestroy());
        }
    }

    private async UniTaskVoid OnClickCallBack()
    {
        float time = Time.time;
        UniTaskCompletionSource source = new UniTaskCompletionSource(); // 可复用
        FallTarget(targetGO.transform, FallTime, onTargetHalf, source).Forget();
        await source.Task;
        Debug.Log($"{targetGO.transform.localScale} {Time.time - time}");
    }

    private void onTargetHalf()
    {
        targetGO.transform.localScale *= 1.5f;
    }
}
