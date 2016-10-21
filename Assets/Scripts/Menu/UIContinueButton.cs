using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIContinueButton : MonoBehaviour {

	private Button continueButton;

	void Awake () {
		continueButton = GetComponent<Button> ();
		Debug.Log (PrefsManager.Instance.GetInt("Level"));
		continueButton.interactable = PrefsManager.Instance.GetInt("Level") > 1;
	}

	public void OnContinue() {
		GameController.Instance.CurrentGameMode = GameMode.Campaign;
		int level = PrefsManager.Instance.GetInt("Level");
		Messenger<int>.Invoke (LevelEvent.loadLevel, level);
	}
}
