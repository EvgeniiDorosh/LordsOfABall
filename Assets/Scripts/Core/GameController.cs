using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	private static GameController instance = null;
	public static GameController Instance {
		get { return instance;}
	}

	public int CurrentLevel { get; set;}

	void Awake() {
		if (instance != null) {
			DestroyImmediate (gameObject);
			return;
		}
			
		instance = this;
		DontDestroyOnLoad (gameObject);
	}

	void OnEnable() {
		AddListeners ();
	}

	void AddListeners () {
		Messenger<int>.AddListener (LevelEvent.loadLevel, LoadLevel);
		Messenger.AddListener (LevelEvent.levelIsComplete, LoadNextLevel);
	}

	void LoadLevel(int levelIndex) {
		CurrentLevel = levelIndex;
		SceneManager.LoadScene (levelIndex);
	}

	void LoadNextLevel() {
		LoadLevel (CurrentLevel + 1);
	}
}
