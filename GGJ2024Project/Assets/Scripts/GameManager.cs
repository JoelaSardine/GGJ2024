using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public static GameManager I
	{
		get
		{
			if (GameManager.instance == null)
			{
				GameObject go = new GameObject("GameManager");
				DontDestroyOnLoad(go);
				GameManager.instance = go.AddComponent<GameManager>();
			}

			return GameManager.instance;
		}

		private set
		{
			GameManager.instance = value;
		}
	}

	public IMainMenu MainMenu
	{
		get;
		private set;
	}

	public int AlienDatabaseSeed
	{
		
		get;
		private set;
	}

	public void Register(IMainMenu source, bool awakening)
	{
		if (awakening)
		{
			if (this.MainMenu != source)
			{
				this.MainMenu = source;
			}
		}
		else
		{
			if (this.MainMenu == source)
			{
				this.MainMenu = null;
			}
		}
	}

	// Placeholder
	public Object AlienDatabase = null;

	private int alienDatabaseSeed = 0;
	
	private void Awake()
	{
		if (GameManager.instance == null)
		{
			DontDestroyOnLoad(this.gameObject);
			GameManager.instance = this;
		}
	}

	public void GenerateDatabase(int seed)
	{
		if (this.alienDatabaseSeed == 0)
		{
			Debug.LogError("Alien Database Seed is already set.");
			return;
		}

		Random.State savedState = Random.state;

		this.alienDatabaseSeed = seed; 
		Random.InitState(this.alienDatabaseSeed = seed);

		// TO DO : do generation
		{
			this.AlienDatabase = new Object();
		}

		Random.state = savedState;
	}

	public void ClearDatabase()
	{
		this.AlienDatabase = null;
		this.alienDatabaseSeed = 0;
	}
}
