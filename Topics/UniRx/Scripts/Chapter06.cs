using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Unity.VisualScripting;
using Unit = UniRx.Unit;

public class Chapter06 : MonoBehaviour
{
    public GameObject cube;

    //UniRx 触发
/*     void Start()
    {
        cube.OnMouseDownAsObservable()
            .Subscribe(_ =>
            {
                Init();
            });
    }
 */
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           Init();
        }
    }

    void Init()
    {
        //正常启用协程
        StartCoroutine(Ie());
        
        //协程 -> Observable
        Ie().ToObservable()
        .DoOnCompleted(() => 
        {
            Debug.Log("Coroutine finished.");
        })
        .Subscribe().AddTo(this);

        //调用方法 1
        StartCoroutine(Obs().DoOnCompleted(() => Debug.Log("Obs Coroutine finished. 1")).ToYieldInstruction());
        
        //调用方法 2
        Obs().DoOnCompleted(() =>Debug.Log("Obs Coroutine finished. 2")).Subscribe().AddTo(this);       
    }


    IEnumerator Ie()
    {
        Debug.Log("1");
        yield return null;
        Debug.Log("2");
    }

    //Observable -> 协程
    IObservable<Unit> Obs()
    {
        IObservable<Unit> returnUnit = Observable.ReturnUnit();
        returnUnit = returnUnit.Do(_ => Debug.Log("111"))
                                .DelayFrame(0)
                                .Do(_ => Debug.Log("222"));
        return returnUnit;
    }
}
