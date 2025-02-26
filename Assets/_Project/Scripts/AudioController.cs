using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private AudioClip wingClip;
    [SerializeField] private AudioSource audioSource;

    public static AudioController Instance;
    private void Start()
    {
        Instance = this;
    }

    public void PlayDieSound()
    {
        audioSource.PlayOneShot(dieClip,0.4f);
    }

    public void PlayWingSound()
    {
        audioSource.PlayOneShot(wingClip,0.2f);
    }
}
