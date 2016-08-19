using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PadelPanelBonusParameterUpdater : MonoBehaviour {

	private Text textField;
	private Outline outlineEffect;
	private string signPlus = "+";

	public string label;

	public Color positiveValueColor;
	public Color negativeValueColor;

	void Awake () {
		textField = GetComponent<Text> ();
		outlineEffect = GetComponent<Outline> ();
	}

	void Start() {
		UpdateText (label, 0f);
	}

	void OnEnable () {
		Messenger<string, float>.AddListener (PaddleEvent.bonusParameterWasUpdated, UpdateText);
	}

	void OnDisable () {
		Messenger<string, float>.RemoveListener (PaddleEvent.bonusParameterWasUpdated, UpdateText);
	}

	private void UpdateText(string label, float value) {
		if (label == this.label) {
			if (value > 0) {
				textField.text = signPlus + Mathf.RoundToInt (value).ToString ();
				outlineEffect.effectColor = positiveValueColor;
			} else if(value < 0) {
				textField.text = Mathf.RoundToInt (value).ToString ();
				outlineEffect.effectColor = negativeValueColor;
			} else {
				textField.color = Color.clear;
			}
		}
	}
}
