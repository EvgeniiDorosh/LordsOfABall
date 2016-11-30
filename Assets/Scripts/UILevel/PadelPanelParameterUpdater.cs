using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PadelPanelParameterUpdater : MonoBehaviour {

	private Text textField;

	public string label;

	void Awake () {
		textField = GetComponent<Text> ();
	}
	
	void OnEnable () {
		Messenger<StatChange>.AddListener (PaddleEvent.parameterWasUpdated, UpdateText);
	}

	void OnDisable () {
		Messenger<StatChange>.RemoveListener (PaddleEvent.parameterWasUpdated, UpdateText);
	}

	void UpdateText(StatChange statChange) {
		if (statChange.name == label) {
			textField.text = Mathf.RoundToInt (statChange.current).ToString();
		}
	}
}
