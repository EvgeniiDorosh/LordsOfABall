using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UILevelButton : MonoBehaviour {

	public int levelIndex;
	public Text levelText;

	void Start() {
		levelText.text = levelIndex.ToString();
	}

	public void OnLevelChoose () {
		int validIndex = levelIndex;
		Messenger<int>.Invoke (LevelEvent.loadLevel, validIndex);
	}
}
