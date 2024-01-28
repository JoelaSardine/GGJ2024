using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInteraction : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private Coroutine routine;
    
    public void Interact()
    {
        if (source.loop)
        {
            if (source.isPlaying)
            {
                StopCoroutine(routine);
                source.Stop();
            }
            else
            {
                routine = StartCoroutine(RefreshInteractor());
                source.Play();
            }
        }
        else
        {
            source.Play();
        }
    }

    IEnumerator RefreshInteractor()
    {
        while (true)
        {
            yield return null;
            SequenceVerifier.Instance.AddUniqueInteraction(GetComponent<Grabable>());
        }
    }
}
