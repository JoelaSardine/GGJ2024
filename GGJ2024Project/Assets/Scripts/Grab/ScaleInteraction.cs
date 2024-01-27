using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleInteraction : MonoBehaviour
{
    [SerializeField] private float scale = 1.25f;

    private Tween tween;
    private Vector3 baseScale;

    private void Awake()
    {
        baseScale = transform.localScale;
    }

    public void Interact()
    {
        if(tween != null) tween.Kill();
        
        transform.localScale = baseScale;
        tween = transform.DOPunchScale(Vector3.one * scale, 0.25f);
    }
}
