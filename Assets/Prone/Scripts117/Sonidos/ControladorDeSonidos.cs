using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeSonidos : MonoBehaviour
{
    public static ControladorDeSonidos InstanceSonidos;
    private AudioSource audioSource;
    [SerializeField] private AudioClip morir;


    private void Awake()
    {
        if(InstanceSonidos == null)
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

    public void EjecutarSonidos(AudioClip sonido, float volumen)
    {
        audioSource.volume = volumen;
        audioSource.PlayOneShot(sonido);
    }

    public void PararSonidos()
    {
        audioSource.Stop();
    }

    public void morirAudio()
    {
        audioSource.PlayOneShot(morir);
    }

}
