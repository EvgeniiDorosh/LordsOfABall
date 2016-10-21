using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIStartButton : MonoBehaviour {

	public void StartGame () {
		GameController.Instance.CurrentGameMode = GameMode.Campaign;
		Messenger<int>.Invoke (LevelEvent.loadLevel, 1);
	}
}
