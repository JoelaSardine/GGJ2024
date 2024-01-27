using UnityEngine;

[CreateAssetMenu(fileName = "NewCultureDefinition", menuName = "GGJ24/CultureDefinition", order = 1)]
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