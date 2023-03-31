using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour
{
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(MusicaBoss());
    }

    IEnumerator MusicaBoss()
    {
        yield return new WaitForSeconds(60);
        audioSource.Play();
    }
}
