using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimInteraction : MonoBehaviour
{
    [SerializeField] private int animationID;
    [SerializeField] private bool loop, destroy;
    
    private Grabable _grabable;

    private void Start()
    {
        _grabable = GetComponent<Grabable>();
    }

    public void Interact(Grabber grabber)
    {
        bool delay = grabber.ForceGrab(_grabable);
        grabber.AnimationInteract(animationID, loop, destroy, delay);
    }
}
