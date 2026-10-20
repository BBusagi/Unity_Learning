using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Chapter01_4 : MonoBehaviour
{
    public Runner BallA_Runner;
    public Runner BallB_Runner;

    public Button BallA_Run;
    public Button BallB_Run;
    public Button BallA_Stop;
    public Button BallB_Stop;
    public Button Reset;


    public float TotalDistance = 15;

    private CancellationTokenSource _ballA_CancelToken;
    private CancellationTokenSource _ballB_CancelToken;
    private CancellationTokenSource _linked_CancelToken;

    private void Start()
    {
        BallA_Run.onClick.AddListener(OnBallARun);
        BallB_Run.onClick.AddListener(OnBallBRun);

        BallA_Stop.onClick.AddListener(OnBallAStop);
        BallB_Stop.onClick.AddListener(OnBallBStop);

        Reset.onClick.AddListener(OnReset);

        _ballA_CancelToken = new CancellationTokenSource();
        _ballB_CancelToken = new CancellationTokenSource();
        _linked_CancelToken = CancellationTokenSource.CreateLinkedTokenSource(_ballA_CancelToken.Token, _ballB_CancelToken.Token);
    }

    private void OnDestroy()
    {
        _ballA_CancelToken.Dispose();
        _ballB_CancelToken.Dispose();
        _linked_CancelToken.Dispose();
    }

    /// <summary>
    /// 方法1 简单 效率低
    /// </summary>
    private async void OnBallARun()
    {
        try
        {
            await RunSomeOne(BallA_Runner, _ballA_CancelToken.Token);
        }
        catch (OperationCanceledException e)
        {
            Debug.Log("Ball A canceled");
        }
    }

    /// <summary>
    /// 方法2 较复杂 效率高
    /// </summary>
    private async void OnBallBRun()
    {
        var (canceled, _) = await RunSomeOne(BallB_Runner, _linked_CancelToken.Token).SuppressCancellationThrow();
        if (canceled)
        {
            Debug.Log("Ball B canceled");
        }
    }

    private void OnBallAStop()
    {
        _ballA_CancelToken.Cancel();
        _ballA_CancelToken.Dispose();
        _ballA_CancelToken = new CancellationTokenSource();
        _linked_CancelToken = CancellationTokenSource.CreateLinkedTokenSource(_ballA_CancelToken.Token, _ballB_CancelToken.Token);
    }

    private void OnBallBStop()
    {
        _ballB_CancelToken.Cancel();
        _ballB_CancelToken.Dispose();
        _ballB_CancelToken = new CancellationTokenSource();
        _linked_CancelToken = CancellationTokenSource.CreateLinkedTokenSource(_ballA_CancelToken.Token, _ballB_CancelToken.Token);
    }

    private void OnReset()
    {
        _ballA_CancelToken.Cancel();
        _ballA_CancelToken = new CancellationTokenSource();
        _ballB_CancelToken = new CancellationTokenSource();
        _linked_CancelToken =
            CancellationTokenSource.CreateLinkedTokenSource(_ballA_CancelToken.Token, _ballB_CancelToken.Token);
        BallA_Runner.Reset();
        BallB_Runner.Reset();
    }

    private async UniTask<int> RunSomeOne(Runner runner, CancellationToken cancellationToken)
    {
        runner.Reset();
        float totalTime = TotalDistance / runner.speed;
        float timeElasped = 0;
        while (timeElasped <= totalTime)
        {
            timeElasped += Time.deltaTime;
            await UniTask.NextFrame(cancellationToken);

            float runDistance = Mathf.Min(timeElasped, totalTime) * runner.speed;
            runner.Target.position = runner.StartPos + Vector3.right * runDistance;
        }
        runner.ReachGoal = true;
        return 0;
    }
}
