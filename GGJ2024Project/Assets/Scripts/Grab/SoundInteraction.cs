using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInteraction : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    
    public void Interact()
    {
        if (source.loop)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
            else
            {
                source.Play();
            }
        }
        else
        {
            source.Play();
        }
    }
}
