using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Header("Control the speed of the Asteroid")]
    public float minSpeed;
    public float maxSpeed;


    [Header("Control the rotationa")]
    public Vector3 movementDirection;
    public float rotationalSpeedMin, rotationalSpeedMax;
    private float rotationalSpeed;
    private float xAngle, yAngle, zAngle;

    private float asteroidSpeed;

    void Start()
    {
        movementDirection = movementDirection.normalized;

        InitializeMovement();
        InitializeRotation();
    }


    void Update()
    {
        transform.Translate(movementDirection * Time.deltaTime * asteroidSpeed, Space.World);
        transform.Rotate(Vector3.up * Time.deltaTime * rotationalSpeed);
    }

    void InitializeMovement()
    {
        asteroidSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void InitializeRotation()
    {
        xAngle = Random.Range(0, 360);
        yAngle = Random.Range(0, 360);
        zAngle = Random.Range(0, 360);
        transform.Rotate(xAngle, yAngle, zAngle);

        rotationalSpeed = Random.Range(rotationalSpeedMin, rotationalSpeedMax);
    }

}
