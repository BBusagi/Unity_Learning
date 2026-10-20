using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Chapter07 : MonoBehaviour
{
 
    void Awake()
    {
        MessageBroker.Default.Receive<Temp>()
                    .SubscribeOnMainThread()    //在主线程中调用
                    .Subscribe(temp => 
                    {
                        Debug.Log($"{temp.index} / {temp.str}");
                    })
                    .AddTo(this);
            
    }

    private void send()
    {
        MessageBroker.Default.Publish( new Temp() 
        {
            index = 111,
            str = "Hello World"
        });
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))     send();
    }
}

public class Temp
{
    public int index;
    public string str;

}
