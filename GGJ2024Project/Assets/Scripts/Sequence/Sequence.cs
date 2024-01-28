using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sequence", menuName = "GGJ24/Sequence", order = 1)]
public class Sequence : ScriptableObject
{
    public List<SequencePart> Parts;
    public MoodType NewMood;

    [Serializable]
    public class SequencePart
    {
        public ActionType Type;
        public ItemType Item;
        public int Number = 1;
    }
}


