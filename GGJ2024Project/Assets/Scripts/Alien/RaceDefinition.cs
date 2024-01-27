using UnityEngine;

[CreateAssetMenu(fileName = "NewRaceDefinition", menuName = "GGJ24/RaceDefinition", order = 1)]
public class RaceDefinition : ScriptableObject
{
	public string Name;
	public RacialFeature[] Features;


	[System.Serializable]
	public class RacialFeature
	{
		// Ex : 2 Big Yellow Noses
		public string Name;
		public RacialSlot Slot;
		public RacialTrait[] Traits;

		public Texture Texture;
	}
}