using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class Chapter04 : MonoBehaviour
{
    private Subject<int> _subject = new Subject<int>();
    private Subject<Tuple<int,float,string>> _tupleSubject = new Subject<Tuple<int,float, string>>();
    private Action<int> _action;
    private Action _emptyAction;

    private UnityEvent _unityEvent = new UnityEvent();
    private event Action _event;

    private void Start()
    {
        /*         _action = index =>
                {
                    Debug.Log("Action " + index);
                };
                // 一般调用方法
                _action?.Invoke(666);
                _action = null;

                //subscribe方法
                _subject.Subscribe(
                    Index =>{
                        Debug.Log("Subject " + Index);
                    }
                ).AddTo(this);
                _subject.OnNext(666); */

        //_unityEvent.AddListener(call);
        
        /*         _unityEvent.AsObservable().Subscribe(_ =>
        { 
            call();    
        }).AddTo(this); */
        //_unityEvent.Invoke();

        Observable.FromEvent(
            action => _event += action,
            action => _event -= action
            ).Subscribe(
                Action =>{
                    call();
                }
            ).AddTo(this);
        _event?.Invoke();

        _tupleSubject.Subscribe(tupleDate =>
        {
            (int item1, float item2, string item3) = tupleDate;
            Debug.Log(item3);
        }).AddTo(this);
        _tupleSubject.OnNext(new Tuple<int, float, string>(0,0f,"tupleData"));

    }

    private void OnDestroy()
    {
        //_unityEvent.RemoveListener(call);
    }

    private void call()
    {
        Debug.Log("[UnityEvent] call");
    }

}
