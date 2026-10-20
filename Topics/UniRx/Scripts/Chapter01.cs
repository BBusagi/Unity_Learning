using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
using Unity.VisualScripting;

public class Chapter01 : MonoBehaviour
{

    [SerializeField] private GameObject Box;
    void Start()
    {
        //传统携程方法
        //StartCoroutine(Ie());

        //UniRx
        Observable.Timer(TimeSpan.FromSeconds(2f))
            .Subscribe(_ => 
            {
                Debug.Log("[UniRx] 2 seconds");
            }).AddTo(this);

        //条件执行
        Observable.EveryUpdate()
            //.Where(_ => Input.GetMouseButtonDown(0))      //鼠标左键时执行
            .Sample(TimeSpan.FromSeconds(1f))               //每1秒执行
            .Subscribe(_ => 
            {
                Debug.Log("[UniRx] Updating");
            })
            .AddTo(this);
        
        Box.OnDisableAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("[BoxDisable] Disable");
            }
            ).AddTo(Box);
    }

    IEnumerator Ie()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("[Coroutine] 2 seconds");
    }

}
