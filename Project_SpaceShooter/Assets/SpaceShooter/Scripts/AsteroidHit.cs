using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHit : MonoBehaviour
{  
    [SerializeField] private GameObject asteroidExplosion;
    public void AsteroidDestoryed()
    {
        Instantiate(asteroidExplosion, transform.position, transform.rotation);

        float distanceFromPlayer = Vector3.Distance(transform.position, Vector3.zero);
        int asteroidScore = 10* (int)distanceFromPlayer;

        GameController.Instance.UpdateGameScore(asteroidScore);

        Destroy(this.gameObject);
    }
}
