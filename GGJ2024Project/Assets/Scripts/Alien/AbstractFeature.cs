using UnityEngine;

[System.ObsoleteAttribute]
public abstract class AbstractFeature
{
	public string Name;
	public Color Color = Color.white;
	public Sprite Sprite = null;
	public Sprite LaughingOverride = null;
	public Sprite HappyOverride = null;
	public Sprite SadOverride = null;
	
	public void ApplyTo(SpriteRenderer renderer, MoodType mood)
	{
		Sprite correctSprite;

		switch (mood)
		{
			case MoodType.Laughing when LaughingOverride != null:
				correctSprite = this.LaughingOverride;
				break;

			case MoodType.Sad when SadOverride != null:
				correctSprite = this.SadOverride;
				break;

			case MoodType.Happy when HappyOverride != null:
				correctSprite = this.HappyOverride;
				break;

			default:
				correctSprite = this.Sprite;
				break;
		}

		renderer.sprite = correctSprite;
		renderer.color = this.Color;

		renderer.enabled = true;
	}
	
	public T GetTrait<T>(int index, string[] serializedTraits, T defaultValue) where T : struct, System.Enum 
	{
		if (System.Enum.TryParse<T>(serializedTraits[index], true, out T match))
		{
			return match;
		}

		return defaultValue;
	}
}