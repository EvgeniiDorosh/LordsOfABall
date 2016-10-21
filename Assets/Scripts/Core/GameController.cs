using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	private static GameController instance = null;
	public static GameController Instance {
		get { return instance;}
	}

	private int currentLevel = 0;
	public int CurrentLevel { 
		get { return currentLevel;}
	}

	private int maxLevel = 0;
	public int MaxLevel {
		get { return maxLevel;}
	}

	private GameMode currentGameMode = GameMode.Menu;
	public GameMode CurrentGameMode {
		get { return currentGameMode;}
		set { currentGameMode = value;}
	}

	void Awake() {
		if (instance != null) {
			DestroyImmediate (gameObject);
			return;
		}
			
		instance = this;
		maxLevel = SceneManager.sceneCount - Settings.levelSceneOffset;
		DontDestroyOnLoad (gameObject);
	}

	void OnEnable() {
		AddListeners ();
	}

	void AddListeners () {
		Messenger<int>.AddListener (LevelEvent.loadLevel, LoadLevel);
		Messenger.AddListener (LevelEvent.levelIsComplete, OnLevelComplete);
	}

	void LoadLevel(int levelIndex) {
		currentLevel = levelIndex;
		SceneManager.LoadScene (levelIndex + Settings.levelSceneOffset);
	}

	void OnLevelComplete() {
		PrefsManager.Instance.SaveCurrentProgress ();
		LoadLevel (currentLevel + 1);
	}
}
