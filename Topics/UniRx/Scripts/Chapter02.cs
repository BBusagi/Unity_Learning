using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Chapter02 : MonoBehaviour
{
    public GameObject cube;
    void Start()
    {

/*         gameObject.OnDisableAsObservable()
        gameObject.UpdateAsObservable()
        gameObject.OnCollisionEnterAsObservable()
        gameObject.OnMouseDownAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("event call");
            }); */

/*         cube.OnMouseDownAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("[Cube] OnMouseDownAsObservable");
            });

        cube.OnDisableAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("[Cube] OnDisableAsObservable");
            });
        
        cube.OnDestroyAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log("[Cube] OnDestroyAsObservable");
            }); */
        
/*         cube.UpdateAsObservable()
            .Sample(TimeSpan.FromSeconds(1f))
            .Subscribe(_ =>
            {
                Debug.Log("Output every 1 second");
            });

        cube.UpdateAsObservable()
            .Where( _ =>
            {
                bool mouse = Input.GetMouseButtonDown(0);
                if(mouse)
                {
                    Debug.Log("Mouse Down");
                    return true;
                }
                else
                {
                    return false;
                }

            } )
            .Sample(TimeSpan.FromSeconds(1f))
            .Delay(TimeSpan.FromSeconds(1f))
            .Subscribe(_ =>
            {
                Debug.Log("Delay 1 second");
            }); */
    }
}
