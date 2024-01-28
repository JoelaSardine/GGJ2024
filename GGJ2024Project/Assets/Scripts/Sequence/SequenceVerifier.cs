using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceVerifier : SingletonMono<SequenceVerifier>
{
    [SerializeField] private float InteractionTimer;
    public UnityEvent<MoodType> OnSequenceValidated;
    
    [Header("Reference")]
    [SerializeField] private ItemDetector Detector;
    [SerializeField] private Grabber Grabber;

    [Space]
    [SerializeField] private List<ItemType> RecordedItemsInteraction = new List<ItemType>();
    private List<Sequence> SequencesToVerify = new List<Sequence>();
    private float timer;
    private int totalItem;

    private void OnEnable()
    {
        Grabber.OnInteractWith.AddListener(OnInteractHandler);
    }

    private void OnDisable()
    {
        Grabber.OnInteractWith.RemoveListener(OnInteractHandler);
    }

    public void SetSequences(List<Sequence> sequences)
    {
        SequencesToVerify = new List<Sequence>(sequences);
    }

    public void OnInteractHandler(Grabable grabable)
    {
        ItemType item = grabable.GetComponent<Item>()?.type;
 
        if (item)
        {
            RecordedItemsInteraction.Add(item);
            timer = 0;
        }
    }

    public void AddUniqueInteraction(Grabable grabable)
    {
        ItemType item = grabable.GetComponent<Item>()?.type;

        if (!RecordedItemsInteraction.Contains(item))
        {
            RecordedItemsInteraction.Add(item);
            timer = 0;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > InteractionTimer)
        {
            RecordedItemsInteraction.Clear();
        }
        
            
        for (int i = 0; i < SequencesToVerify.Count; i++)
        {
            totalItem = 0;
            Sequence sequenceToVerify = SequencesToVerify[i];
            int validParts = 0;
            foreach (var part in sequenceToVerify.Parts)
            {
                if (VerifySequencePart(part)) validParts++;
            }

            if (validParts == sequenceToVerify.Parts.Count && (totalItem == 0 || totalItem == Detector.Items.Count))
            {
                OnSequenceValidated.Invoke(sequenceToVerify.NewMood);
                if (sequenceToVerify.NewMood == MoodType.Laughing)
                {
                    SequencesToVerify.Clear();
                    return;
                }
            }
        }
    }


    private bool VerifySequencePart(Sequence.SequencePart part)
    {
        int num = 0;
        
        switch (part.Type)
        {
            case ActionType.GiveItem :
                foreach (var i in Detector.Items)
                {
                    if (i == part.Item) num++;
                }

                totalItem += part.Number;

                return num == part.Number;
                break;
            
            case ActionType.InteractWithItem :
                foreach (var i in RecordedItemsInteraction)
                {
                    if (i == part.Item) num++;
                }

                return num >= part.Number;
                break;
        }

        return false;
    }
}
