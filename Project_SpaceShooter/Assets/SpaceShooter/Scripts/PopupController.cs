using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    void Update()
    {
        // Utility.Instance.FaceToTarget(transform);
        // Utility.Instance.DestoryGameobject(transform, 3f);

        transform.LookAt(Camera.main.transform);

        Destroy(gameObject, 3f);
    }
}
