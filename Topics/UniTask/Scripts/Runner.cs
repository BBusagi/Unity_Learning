using System;
using UnityEngine;

[Serializable]
public class Runner
{
    public Transform Target;
    public float speed = 5f;
    public Vector3 StartPos;
    public bool ReachGoal = false;

    public void Reset ()
    {
        ReachGoal = false;
        Target.position = StartPos;
    }
}
