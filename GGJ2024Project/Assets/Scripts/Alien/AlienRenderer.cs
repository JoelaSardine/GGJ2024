using System.Collections.Generic;
using UnityEngine;

public class AlienRenderer : MonoBehaviour
{
	[Header("Race slots")]
	[SerializeField] private SpriteRenderer Body;
	[SerializeField] private SpriteRenderer Eyes;
	[SerializeField] private SpriteRenderer Mouth;

	[Header("Culture slots")]
	[SerializeField] private SpriteRenderer Hat;

	[Header("Debug")]
	[SerializeField] private MoodType debugMood;
	[SerializeField] private AlienDefinition debugDefinition = null;

	[System.NonSerialized]
	private List<SpriteRenderer> allRenderers = new ();

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

		// Hide everything by default
		foreach (var spriteRenderer in allRenderers)
		{
			spriteRenderer.enabled = false;
		}

		foreach (var racialFeature in alienDefinition.Race.Features)
		{
			this.Render(racialFeature, mood);
		}

		foreach (var culturalFeature in alienDefinition.Culture.Features)
		{
			this.Render(culturalFeature, mood);
		}
	}

	private void Render(RacialFeature racialFeature, MoodType mood)
	{
		switch (racialFeature.Slot)
		{
			case RacialSlot.Body:
				racialFeature.ApplyTo(this.Body, mood);
				break;

			case RacialSlot.Eye:
				racialFeature.ApplyTo(this.Eyes, mood);
				break;


			case RacialSlot.Mouth:
				racialFeature.ApplyTo(this.Mouth, mood);
				break;

			case RacialSlot.Invalid:
				Debug.LogWarning($"Racial feature {racialFeature.Name} has invalid slot.");
				break;

			default:
				Debug.LogError($"Racial Slot {racialFeature.Slot} has no Renderer. Please add it.");
				break;

		}
	}

	private void Render(CulturalFeature culturalFeature, MoodType mood)
	{
		switch (culturalFeature.Slot)
		{
			case CulturalSlot.Hat:
				culturalFeature.ApplyTo(this.Hat, mood);
				break;

			case CulturalSlot.Invalid:
				Debug.LogWarning($"Cultural feature {culturalFeature.Name} has invalid slot.");
				break;

			default:
				Debug.LogError($"Cultural Slot {culturalFeature.Slot} has no Renderer. Please add it.");
				break;

		}
	}

	private void Start()
	{
		// Race slots
		this.allRenderers.Add(this.Body);
		this.allRenderers.Add(this.Eyes);
		this.allRenderers.Add(this.Mouth);

		// Culture slots
		this.allRenderers.Add(this.Hat);
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