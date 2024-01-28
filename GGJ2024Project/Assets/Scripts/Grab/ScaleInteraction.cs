using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleInteraction : MonoBehaviour
{
    [SerializeField] private float scale = 1.25f, duration = 0.5f;
    [SerializeField] private bool loop;

    private DG.Tweening.Sequence tween;
    private Vector3 baseScale;

    private void Awake()
    {
        baseScale = transform.localScale;
    }

    public void Interact()
    {
        if (tween != null)
        {
            tween.Kill();
            tween = null;
            if (loop)
            {
                transform.localScale = baseScale;
                return;
            }
        }
        
        transform.localScale = baseScale;
        tween = DOTween.Sequence();
        tween.Append(transform.DOPunchScale(Vector3.one * scale, duration));
        if (loop)
        {
            tween.AppendInterval(duration);
            tween.SetLoops(-1);
        }
    }
}
