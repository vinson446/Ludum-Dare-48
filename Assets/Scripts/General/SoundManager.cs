using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bgm;
    public AudioClip bgm2;
    [SerializeField] AudioClip[] clips;

    [Header("In Game")]
    [SerializeField] AudioClip[] hurtSFX;
    [SerializeField] AudioClip[] deathSFX;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            GameManager.instance.audioSource.volume = 0.5f;
        else
            GameManager.instance.audioSource.volume = 0.2f;

        if (SceneManager.GetActiveScene().buildIndex == 2)
            GameManager.instance.audioSource.volume = 0.05f;

        GameManager.instance.audioSource.clip = bgm;
        GameManager.instance.audioSource.Play();
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
