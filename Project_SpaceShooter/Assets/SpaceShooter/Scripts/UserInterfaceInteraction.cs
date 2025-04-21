using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInterfaceInteraction : MonoBehaviour, IRaycast
{
    public UnityEvent onHitByRaycast;
    public void HitByRaycast()
    {
        onHitByRaycast.Invoke();
    }
}
