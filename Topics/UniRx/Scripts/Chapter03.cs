using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class Chapter03 : MonoBehaviour
{
    public GameObject cube;
    private IDisposable _disposable;
    private CompositeDisposable _compositeDisposable;
    public GameObject cube2;

    void Start()
    {
/*         _disposable = cube.UpdateAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("[Cube] Updating");
            }); */

        cube.OnMouseEnterAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("MouseEnter");
                Test();
            });

        _compositeDisposable = new CompositeDisposable();
        cube.UpdateAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("Updating");
            })
            .AddTo(_compositeDisposable);

        _compositeDisposable.AddTo(cube2);
        
    }


    private void Test()
    {
        //_disposable.Dispose();
        _compositeDisposable.Dispose();
    }


}
