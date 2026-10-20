using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Chapter02_2 : MonoBehaviour
{
    public const float G = 9.8f;

    public Button Button;

    public GameObject cube;
    public GameObject sphere;

    public float FallTime1 = 2f;
    public float FallTime2 = 2f;
    
    void Start()
    {
        Button.onClick.AddListener(OnClickStart);
    }

    private async UniTaskVoid FallTarget (Transform targetTrans, float falltime)
    {
        float startTime = Time.time;
        Vector3 startPos = targetTrans.position;
        while(Time.time - startTime <= falltime)
        {
            float elapsedTime = Mathf.Min(Time.time - startTime, falltime);
            float fallY = 0 + 0.5f * G * elapsedTime * elapsedTime;
            targetTrans.position = startPos + Vector3.down * fallY;
            await UniTask.Yield(this.GetCancellationTokenOnDestroy());
        }
    }

    private void OnClickStart()
    {
        FallTarget(cube.transform, FallTime1).Forget();
        FallTarget(sphere.transform, FallTime2).Forget();
    }
}
