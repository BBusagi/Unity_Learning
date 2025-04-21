using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LaserGun : MonoBehaviour
{
    
    [SerializeField] private Animator laserAnimator;
    [SerializeField] private AudioClip laserSFX;
    [SerializeField] private Transform raycastOrigin;

    private AudioSource laserAudioSource;
    private RaycastHit hit;

    void Awake()
    {
        laserAudioSource = GetComponent<AudioSource>();
    }
    public void LaserGunFired()
    {
        // animation and sfx
        laserAnimator.SetTrigger("Fire");
        laserAudioSource.PlayOneShot(laserSFX);

        //raycast
        if(Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, 800f))
        {
            if(hit.transform.GetComponent<AsteroidHit>() != null)
            {
                hit.transform.GetComponent<AsteroidHit>().AsteroidDestoryed();
            }
            else if(hit.transform.GetComponent<IRaycast>() != null)
            {
                hit.transform.GetComponent<IRaycast>().HitByRaycast();
            }
        }

    }
}
