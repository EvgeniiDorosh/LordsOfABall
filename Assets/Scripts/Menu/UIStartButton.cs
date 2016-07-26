using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIStartButton : MonoBehaviour {

	public void StartGame () {
		SceneManager.LoadScene ("Stage1");
	}
}
