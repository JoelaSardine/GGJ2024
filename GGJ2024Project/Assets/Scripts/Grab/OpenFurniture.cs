using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFurniture : MonoBehaviour
{
    private bool open;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Trigger()
    {
        open = !open;
        animator.SetBool("Opened", open);
    }
}
