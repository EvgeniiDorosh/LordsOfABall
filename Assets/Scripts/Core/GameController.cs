using UnityEngine;
using UnityEngine.SceneManagement;
using System;
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
		maxLevel = SceneManager.sceneCountInBuildSettings - Settings.levelSceneOffset;
		DontDestroyOnLoad (gameObject);
	}

	void OnEnable() {
		AddListeners ();
	}

	void AddListeners () {
		Messenger<int>.AddListener (LevelEvent.loadLevel, LoadLevel);
		Messenger.AddListener (LevelEvent.levelIsComplete, OnLevelComplete);
		Messenger.AddListener (LevelEvent.levelIsFailed, OnLevelFailed);
	}

	void LoadLevel(int levelIndex) {
		currentLevel = levelIndex;
		SceneManager.LoadScene (levelIndex + Settings.levelSceneOffset, LoadSceneMode.Single);
	}

	void OnLevelComplete() {
		PrefsManager.Instance.SaveCurrentProgress ();
		int nextLevel = currentLevel + 1;
		if (nextLevel < maxLevel) {
			LoadLevel (nextLevel);
		} else {
			LoadMenu ();
		}
	}

	void OnLevelFailed() {
		PrefsManager.Instance.ClearProgress ();
		LoadMenu ();
	}

	void LoadMenu() {
		CurrentGameMode = GameMode.Menu;
		SceneManager.LoadScene (1, LoadSceneMode.Single);
	}
}
