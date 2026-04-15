using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyWhenAudioFinishes : MonoBehaviour
{
    /* Este Script se encarga de destruir el GameObject cuando terminó de reproducir el sonido.
     * La idea es asignarlo a los GameObject que se encarguen de reproducir efectos de sonido. 
     * 
     */

    private AudioSource audioSource;
    private bool hasStarted = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!hasStarted)
        {
            if (audioSource.isPlaying)
            {
                //Esto es para detectar que el audio ha empezado a reproducirse
                //y no destruir el objeto antes de que eso ocurra
                hasStarted = true;
            }
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                //Cuando el audio termina el objeto se destruye
                Destroy(gameObject);
            }
        }
        
    }
}
