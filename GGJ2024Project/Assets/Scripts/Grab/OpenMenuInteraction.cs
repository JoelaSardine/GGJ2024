using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class OpenMenuInteraction : MonoBehaviour
{
    [SerializeField] private FirstPersonController controller;
    [SerializeField] private Grabber grabber;
    [SerializeField] private StarterAssetsInputs inputs;
    [SerializeField] private GameObject gameObject;

    public void Interact()
    {
        controller.enabled = false;
        grabber.enabled = false;
        inputs.SetCursorState(false);
        gameObject.SetActive(true);
    }
}
