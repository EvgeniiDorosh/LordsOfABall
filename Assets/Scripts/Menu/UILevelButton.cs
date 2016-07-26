using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UILevelButton : MonoBehaviour {

	public int levelIndex;
	public Text levelText;

	void Start() {
		levelText.text = levelIndex.ToString();
	}

	public void LoadScene () {
		int validIndex = levelIndex + Settings.levelSceneOffset;
		SceneManager.LoadScene (validIndex);	
	}
}
