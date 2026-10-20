using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Chapter01_2 : MonoBehaviour
{
    private ReactiveProperty<int> index = new ReactiveProperty<int>(1);
    void Start()
    {
        index.Value++;

        index.Skip(1)
             .Subscribe(_ =>
            {
                Debug.Log("Int Changed");
            }).AddTo(this);
    }
}