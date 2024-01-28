using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public MoodType BaseMood;
    public List<Sequence> Sequences;
    public AlienDefinition AlienDef;

    [Header("Reference")]
    [SerializeField] private AlienRenderer renderer;
    
    private SequenceVerifier verifier;
    private Transform OutPoint;

    public void Init(Transform inPoint, Transform wait, Transform outPoint)
    {
        verifier = SingletonMono<SequenceVerifier>.Instance;
        transform.position = inPoint.position;
        OutPoint = outPoint;
        SetMood(BaseMood);
        Sequences.Clear();
        Sequences.Add(AlienDef.LaughingSequence);
        Sequences.AddRange(AlienDef.Race.Reactions);
        Sequences.AddRange(AlienDef.Culture.Reactions);
        verifier.SetSequences(Sequences);
        verifier.OnSequenceValidated.AddListener(SetMood);
        transform.DOMove(wait.position, 2.0f);
    }
    
    public void SetMood(MoodType mood)
    {
        renderer.Render(AlienDef, mood);

        if (mood == MoodType.Laughing)
        {
            Leave();
        }
    }

    private void Leave()
    {
        transform.DOMove(OutPoint.position, 2.0f).SetDelay(2.0f).onComplete = () => {Destroy(gameObject);};
        verifier.OnSequenceValidated.RemoveListener(SetMood);
    }
}
