using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LaserGun : MonoBehaviour
{
    [SerializeField] private Animator laserAnimator;
    [SerializeField] private AudioClip laserSFX;

    private AudioSource laserAudioSource;

    void Awake()
    {
        laserAudioSource = GetComponent<AudioSource>();
    }
    public void LaserGunFired()
    {
        laserAnimator.SetTrigger("Fire");
        laserAudioSource.PlayOneShot(laserSFX);
    }
}
