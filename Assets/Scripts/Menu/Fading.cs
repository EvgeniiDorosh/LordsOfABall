using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Fading : MonoBehaviour {

	private Animator animator;

	void Awake() {
		DontDestroyOnLoad (gameObject);
		animator = GetComponent<Animator> ();
	}

	void OnEnable() {
		SceneManager.sceneLoaded += FadeOut;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= FadeOut;
	}

	void FadeIn() {
		
	}

	public void OnFadeInOver() {
	
	}

	void FadeOut(Scene scene, LoadSceneMode loadMode) {
		animator.SetTrigger ("fadeOut");
	}

	public void OnFadeOutOver() {
		
	}
}
