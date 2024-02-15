using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppControllerSounds : MonoBehaviour
{
    [SerializeField] AudioSource effectsSource;
    [SerializeField] AudioSource backgroundSource;

    [SerializeField] List<AudioClip> ambientSounds;
    [SerializeField] List<AudioClip> whoosh;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip defeat;

    public void Init()
    {
        StartCoroutine(PlayAudioSequentially());
    }

    private IEnumerator PlayAudioSequentially()
    {
        ambientSounds.Shuffle();

        yield return null;

        foreach (var clip in ambientSounds)
        {
            backgroundSource.clip = clip;
            backgroundSource.Play();
            while (backgroundSource.isPlaying)
            {
                yield return null;
            }
        }
    }

    public void Whoosh()
    {
        var i = Random.Range(0, whoosh.Count);
        effectsSource.PlayOneShot(whoosh[i]);
    }

    public void PlayVictory()
    {
        effectsSource.PlayOneShot(victory);
    }

    public void PlayDefeat()
    {
        effectsSource.PlayOneShot(defeat);
    }
}
