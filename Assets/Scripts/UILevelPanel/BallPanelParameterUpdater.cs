using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallPanelParameterUpdater : MonoBehaviour {

	private Text textField;

	public string label;

	void Awake () {
		textField = GetComponent<Text> ();
	}
	
	void OnEnable () {
		Messenger<string, float>.AddListener (BallEvent.parameterWasUpdated, UpdateText);
	}

	void OnDisable () {
		Messenger<string, float>.RemoveListener (BallEvent.parameterWasUpdated, UpdateText);
	}

	void UpdateText(string label, float value) {
		if (label == this.label) {
			textField.text = Mathf.RoundToInt (value).ToString();
		}
	}
}
