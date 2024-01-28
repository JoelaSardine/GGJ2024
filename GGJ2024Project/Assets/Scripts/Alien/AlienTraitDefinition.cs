using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAlienTrait", menuName = "GGJ24/AlienTraitDefinition", order = 1)]
public class AlienTraitDefinition : ScriptableObject
{
	public string Name;
	
	// Not used in game, it is here more to help integration
	public AlienTraitType TraitType = AlienTraitType.Unspecified;

	public MoodType UsualMood = MoodType.Neutral;

	public AlienFeature[] Features;
	public List<Sequence> Reactions;
}

[System.Serializable]
public class AlienFeature
{
	public string Name = string.Empty;

	public AlienTraitSlot Slot = AlienTraitSlot.Invalid;
	
	public Color Color = Color.white;
	
	public Sprite Sprite = null;
	public ColorOverride[] ColorOverrides;
	public TextureOverride[] TextureOverrides;

	public Sprite GetSpriteFor(MoodType mood)
	{
		foreach (var textureOverride in this.TextureOverrides)
		{
			if (textureOverride.Mood == mood)
			{
				if (textureOverride.Value == null)
				{
					Debug.LogError($"Incorrect Texture Override in Alien Feature '{this.Name}' for Mood '{mood}'.");
				}
				else
				{
					return textureOverride.Value;
				}

				break;
			}
		}

		return this.Sprite;

	}

	public Color GetColorFor(MoodType mood)
	{
		foreach (var colorOverride in this.ColorOverrides)
		{
			if (colorOverride.Mood == mood)
			{
				return colorOverride.Value;
			}
		}

		return this.Color;
	}
}

[System.Serializable]
public class TextureOverride
{
	public MoodType Mood = MoodType.Neutral;
	public Sprite Value = null;
}


[System.Serializable]
public class ColorOverride
{
	public MoodType Mood = MoodType.Neutral;
	public Color Value = Color.white;
}