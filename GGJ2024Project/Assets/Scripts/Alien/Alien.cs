using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using DG.Tweening;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public Mood BaseMood;
    public List<Sequence> Sequences;

    [Header("Reference")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SequenceVerifier verifier;
    [SerializeField] private Mood leavingMood;
    
    private Transform OutPoint;

    public void Init(Transform inPoint, Transform wait, Transform outPoint)
    {
        verifier = SingletonMono<SequenceVerifier>.Instance;
        transform.position = inPoint.position;
        OutPoint = outPoint;
        SetMood(BaseMood);
        verifier.SetSequences(Sequences);
        verifier.OnSequenceValidated.AddListener(SetMood);
        transform.DOMove(wait.position, 5.0f);
    }
    
    public void SetMood(Mood mood)
    {
        renderer.sprite = mood.MoodSprite;

        if (mood == leavingMood)
        {
            Leave();
        }
    }

    private void Leave()
    {
        transform.DOMove(OutPoint.position, 5.0f).SetDelay(2.0f).onComplete = () => {Destroy(gameObject);};
        verifier.OnSequenceValidated.RemoveListener(SetMood);
    }
}
