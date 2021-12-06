using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance;
    public AudioSource audioSource;
    void Awake()
    {
        instance=this;
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySoundFX(AudioClip audioClip,float volume)
    {
        audioSource.PlayOneShot(audioClip,volume);
    }
}
