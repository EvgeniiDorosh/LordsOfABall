using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {

	[Range(0.1f, 5f)]
	public float fadeInTime;

	public Image fadeImage;
	private Color currentColor;

	void Awake() {
		DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded () {		
		currentColor = Color.black;
	}
	
	void Update () {
		if (Time.timeSinceLevelLoad < fadeInTime) {
			float alpha = Time.deltaTime / fadeInTime;
			currentColor.a -= alpha;
			fadeImage.color = currentColor;
		}
	}
}
