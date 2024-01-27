using TMPro;
using UnityEngine;

/// <summary>Singleton. Accessible in Main scene (Outgame) or by pausing the game.</summary>
public class MainMenu : MonoBehaviour, IMainMenu
{
	public bool IsInGame = true;
	
	public ConfirmationPopup ConfirmationPopup;
	
	public TMP_Text SeedText;
	public TMP_InputField SeedInputField;

	public GameObject[] InGameObjects;
	public GameObject[] OutGameObjects;

	void IMainMenu.Show()
	{
		this.RefreshObjectsVisibility();
		this.RefreshSeed();

		this.gameObject.SetActive(true);
	}

	private void LoadScene(string scene)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
	}

	private void QuitApplication()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}

	private void RefreshObjectsVisibility()
	{
		if (this.IsInGame)
		{
			foreach (var outGameObject in OutGameObjects)
			{
				outGameObject.SetActive(false);
			}

			foreach (var inGameObject in InGameObjects)
			{
				inGameObject.SetActive(true);
			}
		}
		else
		{
			foreach (var inGameObject in InGameObjects)
			{
				inGameObject.SetActive(false);
			}

			foreach (var outGameObject in OutGameObjects)
			{
				outGameObject.SetActive(true);
			}
		}
	}

	private void RefreshSeed(bool generateNew = false, bool fromInputField = false)
	{
		int seed;
		if (this.IsInGame)
		{
			seed = GameManager.I.AlienDatabaseSeed;
		}
		else if (generateNew || !int.TryParse(this.SeedInputField.text, out seed))
		{
			// Inputfield character limit is 9
			seed = Random.Range(-99999999, 999999999);
		}

		string formattedSeed = seed.ToString(seed > 0 ? "D9" : "D8");
		this.SeedText.text = formattedSeed;

		if (!fromInputField)
		{
			this.SeedInputField.text = formattedSeed;
		}
	}

	// ReSharper disable UnusedMember.Global
	private void Awake()
	{
		GameManager.I.Register(this, true);

		this.RefreshObjectsVisibility();
	}

	private void OnDestroy()
	{
		GameManager.I.Register(this, false);
	}

	public void Button_GenerateSeed()
	{
		this.RefreshSeed(generateNew: true);
	}

	public void Button_CopySeed()
	{
		string seed;
		if (this.IsInGame)
		{
			seed = GameManager.I.AlienDatabaseSeed.ToString();
		}
		else
		{
			seed = this.SeedText.text;
		}

		GUIUtility.systemCopyBuffer = seed;
	}

	public void SeedTextField_OnEndEdit(string text)
	{
		this.RefreshSeed(fromInputField: true);
	}

	public void Button_LaunchScene(string scene)
    {
		this.LoadScene(scene);
    }

    public void Button_MainMenu()
    {
	    this.ConfirmationPopup.Bind("You will leave the current game and lose your progress. Are you sure?", () =>
	    {
		    this.LoadScene("Main");
	    });
    }

    public void Button_Quit()
    {
	    this.ConfirmationPopup.Bind("You're about to quit the game. Do you confirm?", this.QuitApplication);
    }
    // ReSharper restore UnusedMember.Global
}
