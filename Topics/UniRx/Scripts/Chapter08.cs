using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class Chapter08 : MonoBehaviour
{
    //一般方法
/*     private int _index;
    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;
            _action?.Invoke(value);
        }
    }

    private Action<int> _action; */

    private ReactiveProperty<int> _indexRP = new ReactiveProperty<int>();
    private ReactiveProperty<string> _stringRP = new ReactiveProperty<string>("Riku");
    //ReactiveProperty<GameObject> _go = new ReactiveProperty<GameObject>();
    void Start()
    {
        //标准用法
        _indexRP.Skip(1)
        .Delay(TimeSpan.FromSeconds(1))
        .Subscribe(_ =>
        {
            Debug.Log($"_indexRP value changed {_}");
        }).AddTo(this);

        //合并两个事件流
        _indexRP.Select(_ => Unit.Default)
                .Merge(_stringRP.Select(_ => Unit.Default))
                .Subscribe(_ =>
                {
                    Debug.Log("Triggered");
                }).AddTo(this);

        _indexRP.Value = 0;   //和当前值一致的时候不会触发
        _indexRP.SetValueAndForceNotify(0); //设置并强制触发
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Add();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StringChange();
        }
    }

    private void Add()
    {
        _indexRP.Value ++;
    }

    private void StringChange()
    {
        _stringRP.Value = Guid.NewGuid().ToString();
    }
}
