using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource gameManagerAudioSource;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip[] clips;

    [Header("In Game")]
    [SerializeField] AudioClip[] hurtSFX;
    [SerializeField] AudioClip[] deathSFX;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.audioSource.clip = bgm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void PlaySoundClip(int index)
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);

        audioSource.PlayOneShot(clips[index]);
    }

    public void PlayHurtClip()
    {
        int i = Random.Range(0, clips.Length - 1);

        audioSource.pitch = Random.Range(0.95f, 1.05f);

        audioSource.PlayOneShot(hurtSFX[i]);
    }

    public void PlayDeathClip()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);

        int deaths = GameManager.instance.numDeaths;
        if (deaths > 2)
            deaths = 2;

        audioSource.PlayOneShot(deathSFX[deaths]);
    }
}
