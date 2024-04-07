using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSondOnAwake : MonoBehaviour
{

    public List<AudioClip> audioClips;

    private AudioSource thisAudioSouce;

    void Awake()
    {
        // Audio
        thisAudioSouce = GetComponent<AudioSource>();
    }

    // -- START-- //
    void Start()
    {
        var audioClip = audioClips[Random.Range(0, audioClips.Count)];
        thisAudioSouce.PlayOneShot(audioClip);
    }
}
