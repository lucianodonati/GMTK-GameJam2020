using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audios;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private Vector2 timer;

    [SerializeField]
    List<AudioClip> audiosList = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var audio in audios)
        {
            audiosList.Add(audio);
        }

        StartCoroutine(PlayRandomSounds());
    }

    private void ResetPlayList()
    {
        foreach (var audio in audios)
        {
            audiosList.Add(audio);
        }

        StopCoroutine(PlayRandomSounds());
        StartCoroutine(PlayRandomSounds());
    }

    private IEnumerator PlayRandomSounds()
    {
        while (audiosList.Count > 0)
        {
            if (!source.isPlaying)
            {
                yield return new WaitForSeconds(Random.Range(timer.x, timer.y));
                var audioIndex = Random.Range(0, audiosList.Count);
                source.clip = audiosList[audioIndex];
                source.Play();
                audiosList.Remove(audiosList[audioIndex]);
            }

            yield return null;
        }

        if (audiosList.Count <= 0)
        {
            ResetPlayList();
        }
    }
}