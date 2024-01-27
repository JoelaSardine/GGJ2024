using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceVerifier : MonoBehaviour
{
    [SerializeField] private List<Sequence> SequencesToVerify;
    [SerializeField] private float InteractionTimer;
    
    [Header("Reference")]
    [SerializeField] private ItemDetector Detector;
    [SerializeField] private Grabber Grabber;

    [SerializeField] private List<ItemType> RecordedItemsInteraction = new List<ItemType>();
    private float timer;

    private void OnEnable()
    {
        Grabber.OnInteractWith.AddListener(OnInteractHandler);
    }

    private void OnDisable()
    {
        Grabber.OnInteractWith.RemoveListener(OnInteractHandler);
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

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > InteractionTimer)
        {
            RecordedItemsInteraction.Clear();
        }

        List<int> SequenceToRemove = new List<int>();
            
        for (int i = 0; i < SequencesToVerify.Count; i++)
        {
            Sequence sequenceToVerify = SequencesToVerify[i];
            int validParts = 0;
            foreach (var part in sequenceToVerify.Parts)
            {
                if (VerifySequencePart(part)) validParts++;
            }

            if (validParts == sequenceToVerify.Parts.Count)
            {
                Debug.Log(sequenceToVerify.name + " Is Validated");
                SequenceToRemove.Add(i);
            }
        }

        foreach (var i in SequenceToRemove)
        {
            SequencesToVerify.RemoveAt(i);
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

                return num == part.Number;
                break;
            
            case ActionType.InteractWithItem :
                foreach (var i in RecordedItemsInteraction)
                {
                    if (i == part.Item) num++;
                }

                return num == part.Number;
                break;
        }

        return false;
    }
}
