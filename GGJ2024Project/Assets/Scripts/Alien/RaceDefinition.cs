using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRaceDefinition", menuName = "GGJ24/RaceDefinition", order = 1)]
public class RaceDefinition : ScriptableObject
{
	public string Name;
	public RacialFeature[] Features;
	public List<Sequence> Reactions;
}

[System.Serializable]
public class RacialFeature : AbstractFeature
{
	public RacialSlot Slot;
	public RacialTrait[] Traits;
}