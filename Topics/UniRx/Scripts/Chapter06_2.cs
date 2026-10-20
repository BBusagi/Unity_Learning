using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Unity.VisualScripting;
using Unit = UniRx.Unit;

public class Chapter06_2 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Init();
        }
    }

    void Init()
    {
        StartCoroutine(Obs().DoOnCompleted(() => Debug.Log("Obs Coroutine finished. 1")).ToYieldInstruction());

        Ie().ToObservable()
            .Catch<Unit,Exception>(_ =>
            {
                Debug.LogError(_);
                return Observable.ReturnUnit();
            })
            .DoOnCompleted(() => Debug.Log("finished"))
            .Subscribe()
            .AddTo(this);

    }

    IEnumerator Ie()
    {
        int index = 0;
        Debug.Log("1");
        yield return null;
        Debug.Log("2");
        int[] array = new int[0];
        index = array[0];
        Debug.Log("---");
    }

    IObservable<Unit> Obs()
    {
        int index = 0;
        IObservable<Unit> returnUnit = Observable.ReturnUnit();
        returnUnit = returnUnit.Do(_ => Debug.Log("111"))
                                .DelayFrame(0)
                                .Do(_ => Debug.Log("222"))
                                .Do(_ =>
                                {
                                    int[] array = new int[0];
                                    index = array[0];
                                    Debug.Log("---");
                                })
                                .Catch<Unit, Exception>(exception =>
                                {
                                    index = -1;
                                    Debug.LogError($"Index = {index}");
                                    return Observable.ReturnUnit();
                                });

        return returnUnit;
    }
}
