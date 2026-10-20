using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;
using UnityEngine.UI;

public class Chapter01_3 : MonoBehaviour
{
    public Runner BallA_Runner;
    public Runner BallB_Runner;
    
    public Button BallA_Button;
    public Button BallB_Button;

    public Button WhenAll;
    public Button WhenAny;
    public Button Reset;

    public float TotalDistance = 15;

    void Start()
    {
        BallA_Button.onClick.AddListener(OnClickBallA);
        BallB_Button.onClick.AddListener(OnClickBallB);

        WhenAll.onClick.AddListener(OnClickWhenAll);
        WhenAny.onClick.AddListener(OnClickWhenAny);
        Reset.onClick.AddListener(OnClickReset);

    }

    private void OnClickReset()
    {
        BallA_Runner.Reset();
        BallB_Runner.Reset();
    }

    private async UniTask RunBall(Runner runner)
    {
        runner.Reset();
        float totalTime = TotalDistance/runner.speed;
        float timeElasped = 0;
        while (timeElasped <= totalTime)
        {
            timeElasped += Time.deltaTime;
            await UniTask.NextFrame();
            float runDistance = Mathf.Min(timeElasped,totalTime) * runner.speed;
            runner.Target.position = runner.StartPos + Vector3.right * runDistance;
        }

        runner.ReachGoal = true;
    }

    private async void OnClickBallA()
    {
        await RunBall(BallA_Runner);
    }

        private async void OnClickBallB()
    {
        await RunBall(BallB_Runner);
    }

    private async void OnClickWhenAll()
    {
        //WaitUntilValueChanged
        
        var firstRunner = UniTask.WaitUntil(() => BallA_Runner.ReachGoal);
        var secondRunner = UniTask.WaitUntil(() => BallB_Runner.ReachGoal);
        await UniTask.WhenAll(firstRunner,secondRunner);

        Debug.Log("All players arrived");
    }

    private async void OnClickWhenAny()
    {
        var firstRunner = UniTask.WaitUntil(() => BallA_Runner.ReachGoal);
        var secondRunner = UniTask.WaitUntil(() => BallB_Runner.ReachGoal);
        await UniTask.WhenAny(firstRunner,secondRunner);

        string winner = BallA_Runner.ReachGoal ? "Ball A" : "Ball B";
        Debug.Log($"{winner} first arrived");
    }
}
