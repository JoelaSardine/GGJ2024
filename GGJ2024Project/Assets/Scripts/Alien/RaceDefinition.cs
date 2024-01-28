using System.Collections.Generic;
using UnityEngine;

[System.ObsoleteAttribute]
public class RaceDefinition : ScriptableObject
{
	public string Name;
	public RacialFeature[] Features;
	public List<Sequence> Reactions;
}

[System.ObsoleteAttribute]
[System.Serializable]
public class RacialFeature : AbstractFeature
{
	public RacialSlot Slot;
	public RacialTrait[] Traits;
}