using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public Transform gunMuzzle;
    public float maxDistance = 100f;
    public LayerMask hitLayers;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.startWidth = 0.001f;
        lr.endWidth = 0.002f;
    }

    void Update()
    {
        if (gunMuzzle == null) return;

        RaycastHit hit;
        Vector3 endPoint = gunMuzzle.position + gunMuzzle.forward * maxDistance;

        if (Physics.Raycast(gunMuzzle.position, gunMuzzle.forward, out hit, maxDistance, hitLayers))
        {
            endPoint = hit.point;
        }

        lr.SetPosition(0, gunMuzzle.position);
        lr.SetPosition(1, endPoint);
    }
}
