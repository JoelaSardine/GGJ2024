using UnityEngine;

[CreateAssetMenu(fileName = "NewRaceDefinition", menuName = "GGJ24/RaceDefinition", order = 1)]
public class RaceDefinition : ScriptableObject
{
	public string Name;
	public RacialFeature[] Features;
}

[System.Serializable]
public class RacialFeature : AbstractFeature
{
	public RacialSlot Slot;
	public RacialTrait[] Traits;
}