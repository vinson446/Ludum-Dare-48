using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
