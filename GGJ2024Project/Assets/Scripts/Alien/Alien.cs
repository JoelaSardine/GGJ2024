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
    private float timer;
    private MoodType CurrentMood;

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

    private void Update()
    {
        if (CurrentMood != BaseMood)
        {
            timer += Time.deltaTime;
            if(timer > 4f) SetMood(BaseMood);
        }
    }

    public void SetMood(MoodType mood)
    {
        renderer.Render(AlienDef, mood);
        CurrentMood = mood;
        timer = 0;

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
