using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    private int currentIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextTrack();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        audioSource.clip = musicTracks[currentIndex];
        audioSource.Play();

        currentIndex += 1;
        if(currentIndex == 4)
        {
            currentIndex = 0;
        }

    }
}
