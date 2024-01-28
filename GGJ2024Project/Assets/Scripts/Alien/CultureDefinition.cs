using UnityEngine;

[CreateAssetMenu(fileName = "NewCultureDefinition", menuName = "GGJ24/CultureDefinition", order = 1)]
public class CultureDefinition : ScriptableObject
{
	public string Name;
	public CulturalFeature[] Features;
}

[System.Serializable]
public class CulturalFeature : AbstractFeature
{
	public CulturalSlot Slot;
	public CulturalTrait[] Traits;
}