using UnityEngine;

public class Utility : MonoSingleton<Utility>
{
    public void FaceToTarget(Transform localTransform, GameObject gameObject = null)
    {
        if (gameObject == null)
        {
            localTransform.LookAt(Camera.main.transform);
        }
        else
        {
            localTransform.LookAt(gameObject.transform);
        }
    } 

    public void DestoryGameobject(Transform localTransform, float time = -1f)
    {
        if(time < 0 ) return;

        Destroy(localTransform.gameObject, time);
    }
}
