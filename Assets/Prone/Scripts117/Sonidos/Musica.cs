using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour
{
    public static Musica InstanceSonidos;
    private AudioSource audioSource;


    private void Awake()
    {
        if (InstanceSonidos == null)
        {
            InstanceSonidos = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void EjecutarMusica(AudioClip sonido, float volumen)
    {
        audioSource.volume = volumen;
        audioSource.loop = true;
        audioSource.PlayOneShot(sonido);
    }

    public void PararMusica()
    {
        audioSource.Stop();
    }
}
