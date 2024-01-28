using System.Collections.Generic;
using UnityEngine;

[System.ObsoleteAttribute]
public class CultureDefinition : ScriptableObject
{
	public string Name;
	public CulturalFeature[] Features;
	public List<Sequence> Reactions;
}

[System.ObsoleteAttribute]
[System.Serializable]
public class CulturalFeature : AbstractFeature
{
	public CulturalSlot Slot;
	public CulturalTrait[] Traits;
}