using UnityEngine;

[CreateAssetMenu(fileName = "NewAlienDefinition", menuName = "GGJ24/AlienDefinition", order = 1)]
public class AlienDefinition : ScriptableObject
{
	public string Name;

	public RaceDefinition Race;
	public CultureDefinition Culture;
	
	public class RaceDefinition : ScriptableObject
	{
		public string Name;
		public RacialFeature[] Features;
	}

	public class CultureDefinition : ScriptableObject
	{
		public string Name;
		public CulturalFeature[] Features;
	}

	[System.Serializable]
	public class CulturalFeature
	{
		// Ex : Red Face Painting
		public string Name;
		public CulturalSlot Slot;
		public CulturalTrait[] Traits;

		public Texture Texture;
	}

	[System.Serializable]
	public class RacialFeature
	{
		// Ex : 2 Big Yellow Noses
		public string Name;
		public RacialSlot Slot;
		public RacialTrait[] Traits;

		public Texture Texture;
	}

	public enum CulturalSlot
	{
		Invalid,
		Face_Paint,
	}

	public enum CulturalTrait
	{
		Unspecified,
		Color_Red,
	}
	public enum RacialSlot
	{
		Invalid = 0,
		Skin,
		Horn,
		Hair,
		Eye,
		Nose,
		Mouth,
		Head,
		Arm,
		Leg,
		Tail,
	}

	public enum RacialTrait
	{
		Unspecified = 0,
		Count,
		Color_Red,
		Color_Yellow,
		Size_Big,
		Size_Small,
	}
}
