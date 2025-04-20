using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Size of the spawner area")]
    public Vector3 spawnerSize;

    [Header("Rate of spawn")]
    public float spawnRate = 0;
    public float spawnTimer = 0;

    [Header("Model to spawn")]
    [SerializeField] private GameObject asteroidModel;

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green * (1, 1, 1, 0.5);
        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);

        Gizmos.DrawCube(transform.position, spawnerSize);       
    }   

    void Start()
    {
        
    }


    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnRate)
        {
            Debug.Log("[Deving] Spawning");
            SpawnAsteroid();
            spawnTimer = 0;
        }
    }

    private void SpawnAsteroid()
    {
        Vector3 spawnPoint = transform.position + new Vector3(
            UnityEngine.Random.Range(-spawnerSize.x/2,  spawnerSize.x/2),
            UnityEngine.Random.Range(-spawnerSize.y/2,  spawnerSize.y/2),
            UnityEngine.Random.Range(-spawnerSize.z/2,  spawnerSize.z/2)
            );
        GameObject asteroid_instance = Instantiate(asteroidModel,spawnPoint, transform.rotation);
        asteroid_instance.transform.SetParent(this.transform);
    }
}
