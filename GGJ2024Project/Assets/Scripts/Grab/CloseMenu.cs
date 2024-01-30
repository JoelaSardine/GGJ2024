using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class CloseMenu : MonoBehaviour
{
    [SerializeField] private FirstPersonController controller;
    [SerializeField] private Grabber grabber;
    [SerializeField] private StarterAssetsInputs inputs;
    [SerializeField] private GameObject gameObject;

    private void Start()
    {
        inputs.SetCursorState(true);
    }

    public void OnEscape()
    {
        controller.enabled = true;
        grabber.enabled = true;
        inputs.SetCursorState(true);
        gameObject.SetActive(false);
    }
}
