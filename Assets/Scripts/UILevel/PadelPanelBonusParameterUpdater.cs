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

	void OnEnable () {
		Messenger<StatChange>.AddListener (PaddleEvent.parameterWasUpdated, UpdateText);
	}

	void OnDisable () {
		Messenger<StatChange>.RemoveListener (PaddleEvent.parameterWasUpdated, UpdateText);
	}

	private void UpdateText(StatChange statChange) {
		if (statChange.name == label) {
			float bonusValue = statChange.current - statChange.initial;
			if (bonusValue > 0) {
				textField.text = signPlus + Mathf.RoundToInt (bonusValue).ToString ();
				textField.color = Color.black;
				outlineEffect.effectColor = positiveValueColor;
			} else if(bonusValue < 0) {
				textField.text = Mathf.RoundToInt (bonusValue).ToString ();
				textField.color = Color.black;
				outlineEffect.effectColor = negativeValueColor;
			} else {
				textField.color = Color.clear;
			}
		}
	}
}
