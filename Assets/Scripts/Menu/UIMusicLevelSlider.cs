using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMusicLevelSlider : MonoBehaviour {
		
	private Slider slider;
	private Image fillImage;

	void Awake() {
		slider = GetComponent<Slider> ();
		fillImage = slider.fillRect.GetComponent<Image> ();
	}

	void Start() {
		slider.onValueChanged.AddListener(delegate {CheckValueChanging();});
		slider.value = Settings.MusicLevel;
		UpdateSlidelColor ();
	}

	void CheckValueChanging() {
		Settings.MusicLevel = (int)slider.value;
		UpdateSlidelColor ();
	}

	void UpdateSlidelColor() {
		fillImage.color = Color.Lerp (Color.red, Color.green, (1 - slider.value / slider.maxValue));
	}
}
