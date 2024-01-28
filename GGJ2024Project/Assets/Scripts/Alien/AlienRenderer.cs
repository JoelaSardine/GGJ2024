using System.Collections.Generic;
using UnityEngine;

public class AlienRenderer : MonoBehaviour
{
	public SpriteRenderer Body, Arm, Horn, Head, Cloth1, Cloth2, Ear, Mouth, Eye, Nose;

	[Header("Debug (Play Mode Only)")]
	[SerializeField] private MoodType debugMood;
	[SerializeField] private AlienDefinition debugDefinition = null;

	[System.NonSerialized]
	private Dictionary<AlienTraitSlot, SpriteRenderer> SlotRenderers = new ();

	private string lastAlien;
	private MoodType lastMood;

	public void Render(AlienDefinition alienDefinition, MoodType mood)
	{
		if (this.lastAlien == alienDefinition.name && this.lastMood == mood)
		{
			return;
		}

		this.lastAlien = alienDefinition.Name;
		this.lastMood = mood;

		this.HideAndUnbindAll();

		foreach (var racialFeature in alienDefinition.Race.Features)
		{
			this.Render(racialFeature, mood);
		}

		foreach (var culturalFeature in alienDefinition.Culture.Features)
		{
			this.Render(culturalFeature, mood);
		}
	}

	private void HideAndUnbindAll()
	{
		foreach (var spriteRenderer in this.SlotRenderers.Values)
		{
			spriteRenderer.sprite = null;
			spriteRenderer.color = Color.white;
			spriteRenderer.enabled = false;
		}
	}

	private void Render(AlienFeature feature, MoodType mood)
	{
		if (!this.SlotRenderers.TryGetValue(feature.Slot, out SpriteRenderer renderer))
		{
			Debug.LogError($"Alien Trait Slot {feature.Slot} has no Renderer. Please add it.");
			return;
		}

		renderer.color = feature.GetColorFor(mood);

		// If no sprite is set, keep the old one to allow Culture to override Race color.
		Sprite newSprite = feature.GetSpriteFor(mood);
		if (newSprite != null)
		{
			renderer.sprite = newSprite;
		}
		
		renderer.enabled = renderer.sprite != null;
	}
	
	private void Start()
	{
		this.SlotRenderers.Add(AlienTraitSlot.Body, this.Body);
		this.SlotRenderers.Add(AlienTraitSlot.Arm, this.Arm);
		this.SlotRenderers.Add(AlienTraitSlot.Horn, this.Horn);
		this.SlotRenderers.Add(AlienTraitSlot.Head, this.Head);
		this.SlotRenderers.Add(AlienTraitSlot.Cloth2, this.Cloth2);
		this.SlotRenderers.Add(AlienTraitSlot.Cloth1, this.Cloth1);
		this.SlotRenderers.Add(AlienTraitSlot.Ear, this.Ear);
		this.SlotRenderers.Add(AlienTraitSlot.Mouth, this.Mouth);
		this.SlotRenderers.Add(AlienTraitSlot.Eye, this.Eye);
		this.SlotRenderers.Add(AlienTraitSlot.Nose, this.Nose);
	}

	private void OnValidate()
	{
		if (this.debugDefinition != null)
		{
			this.Render(this.debugDefinition, this.debugMood);
			this.debugDefinition = null;
		}
	}
}