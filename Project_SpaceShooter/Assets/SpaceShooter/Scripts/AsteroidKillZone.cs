using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidKillZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter");
        if (other.gameObject.CompareTag("Asteroid"))
        {
            other.GetComponent<Animator>().SetTrigger("FadeOut");
            Destroy(other.gameObject,3f);
        }
    }

}
