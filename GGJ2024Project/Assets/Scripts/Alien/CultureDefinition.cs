using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCultureDefinition", menuName = "GGJ24/CultureDefinition", order = 1)]
public class CultureDefinition : ScriptableObject
{
	public string Name;
	public CulturalFeature[] Features;
	public List<Sequence> Reactions;
}

[System.Serializable]
public class CulturalFeature : AbstractFeature
{
	public CulturalSlot Slot;
	public CulturalTrait[] Traits;
}