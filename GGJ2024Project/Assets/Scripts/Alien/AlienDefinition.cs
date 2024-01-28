using UnityEngine;

[CreateAssetMenu(fileName = "NewAlienDefinition", menuName = "GGJ24/AlienDefinition", order = 1)]
public class AlienDefinition : ScriptableObject
{
	public string Name;

	public AlienTraitDefinition Race;
	public AlienTraitDefinition Culture;

	[UnityEngine.Serialization.FormerlySerializedAsAttribute("LaughtingSequence")]
	public Sequence LaughingSequence;
}
