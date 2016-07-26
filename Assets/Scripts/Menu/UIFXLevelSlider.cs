using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFXLevelSlider : MonoBehaviour {
		
	private Slider slider;
	private Image fillImage;

	void Awake() {
		slider = GetComponent<Slider> ();
		fillImage = slider.fillRect.GetComponent<Image> ();
	}

	void Start() {
		slider.onValueChanged.AddListener(delegate {CheckValueChanging();});
		slider.value = Settings.FXLevel;
		UpdateSlidelColor ();
	}

	void CheckValueChanging() {
		Settings.FXLevel = (int)slider.value;
		UpdateSlidelColor ();
	}

	void UpdateSlidelColor() {
		fillImage.color = Color.Lerp (Color.red, Color.green, (1 - slider.value / slider.maxValue));
	}
}
