using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Chapter01_2 : MonoBehaviour
{
    public PlayerLoopTiming TestYieldTiming = PlayerLoopTiming.PreUpdate;
    public Button DelayButton;
    public Button DelayFrameButton;
    public Button YieldButton;
    public Button NextFrameButton;
    public Button EndofFrameButton;

    private UniTaskTools uniTaskWaiter;

    private List<PlayerLoopSystem.UpdateFunction> _injectUpdateFunction = new List<PlayerLoopSystem.UpdateFunction>();

    private bool _showUpdateLog = false;


    void Start()
    {
        DelayButton.onClick.AddListener(OnClickDelay);
        DelayFrameButton.onClick.AddListener(OnClickDelayFrame);
        YieldButton.onClick.AddListener(OnClickYield);
        NextFrameButton.onClick.AddListener(OnClickNextFrame);
        EndofFrameButton.onClick.AddListener(OnClickEndofFrame);
        uniTaskWaiter = new UniTaskTools();

        InjectFuntion();
    }

    private async void OnClickDelay()
    {
        Debug.Log("Delay 1 second");
        Debug.Log(Time.time);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        Debug.Log(Time.time);
    }

    private async void OnClickDelayFrame()
    {
        Debug.Log("Delay 5 frame");
        Debug.Log(Time.frameCount);
        await UniTask.DelayFrame(5);
        Debug.Log(Time.frameCount);
    }
    private async void OnClickYield()
    {
        _showUpdateLog = true;
        Debug.Log("Wait Yield ");
        Debug.Log("[Yield]Start " + TestYieldTiming);
        await uniTaskWaiter.WaitYield(TestYieldTiming);
        Debug.Log("[Yield]End " + TestYieldTiming);
        _showUpdateLog = false;
    }

    private async void OnClickNextFrame()
    {
        _showUpdateLog = true;
        Debug.Log("Wait NextFrame");
        Debug.Log("[NextFrame]Start " + Time.frameCount);
        await uniTaskWaiter.WaitNextFrame();
        Debug.Log("[NextFrame]End " + Time.frameCount);
        _showUpdateLog = false;
    }
    private async void OnClickEndofFrame()
    {
        _showUpdateLog = true;
        Debug.Log("Wait EndofFrame");
        Debug.Log("[EndofFrame]Start " + Time.frameCount);
        await uniTaskWaiter.WaitEndofFrame(this);
        Debug.Log("[EndofFrame]End " + Time.frameCount);
        _showUpdateLog = false;
    }

    /// <summary>
    /// 查看底层系统
    /// </summary>
    private void InjectFuntion()
    {
        PlayerLoopSystem playerLoop = PlayerLoop.GetCurrentPlayerLoop();    //获得所有的更新内容
        var subsystems = playerLoop.subSystemList;
        playerLoop.updateDelegate += OnUpdate;
        for (int i = 0; i< subsystems.Length; i++)
        {
            int index = i;
            PlayerLoopSystem.UpdateFunction injectFunction = () =>
            {
                if (!_showUpdateLog) return;
                //输出当前的子系统
                Debug.Log($"{_showUpdateLog} Subsystem:[{subsystems[index]}] / FC:{Time.frameCount}");
            };
            _injectUpdateFunction.Add(injectFunction);
            subsystems[i].updateDelegate += injectFunction;
        }
        PlayerLoop.SetPlayerLoop(playerLoop);
    }

    private void UnInjectFunction()
    {
        PlayerLoopSystem playerLoop = PlayerLoop.GetCurrentPlayerLoop();
        playerLoop.updateDelegate -= OnUpdate;
        var subsystem = playerLoop.subSystemList;
        for(int i = 0; i < subsystem.Length; i++)
        {
            subsystem[i].updateDelegate -= _injectUpdateFunction[i];
        }

        PlayerLoop.SetPlayerLoop(playerLoop);
        _injectUpdateFunction.Clear();
    }

    private void OnUpdate()
    {
        Debug.Log("Current Framecount " + Time.frameCount);
    }

    private void OnDestory()
    {
        UnInjectFunction();
    }
}
