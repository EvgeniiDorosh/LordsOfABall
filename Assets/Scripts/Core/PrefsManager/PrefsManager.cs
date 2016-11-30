using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public class PrefsManager : MonoBehaviour {

	private static PrefsManager instance = null;
	public static PrefsManager Instance {
		get { return instance;}
	}

	void Awake() {
		if (instance != null) {
			DestroyImmediate (gameObject);
			return;
		}

		instance = this;
		Encryptor.Keys = CryptKey.Convert (PlayerPrefs.GetString("Date"));
		if (CheatingTakesPlace ()) {
			PlayerPrefs.DeleteAll ();		
		}
	}

	public void SetValue(string key, string value) {
		PlayerPrefs.SetString (Encryptor.Encrypt (key), Encryptor.Encrypt (value));
	}

	public void SetValue(string key, int value) {
		PlayerPrefs.SetString (Encryptor.Encrypt (key), Encryptor.Encrypt (value.ToString()));
	}

	public void SetValue(string key, float value) {
		PlayerPrefs.SetString (Encryptor.Encrypt (key), Encryptor.Encrypt (value.ToString()));
	}

	public string GetString(string key) {
		string result = TryToGetValue(key);
		return result;
	}

	public int GetInt(string key) {
		string result = TryToGetValue(key);
		result = (result != "") ? result : "0";
		return int.Parse(result);
	}

	public float GetFloat(string key) {
		string result = TryToGetValue(key);
		result = (result != "") ? result : "0.0";
		return float.Parse(result);
	}

	string TryToGetValue(string key) {
		string result = "";
		string trueKey = Encryptor.Encrypt (key);
		if (PlayerPrefs.HasKey (trueKey)) {
			result = PlayerPrefs.GetString (trueKey);
			result = Encryptor.Decrypt (result);
		}
		return result;
	}

	public void SaveCurrentProgress() {
		DateTime date = DateTime.Now;
		string keyText = date.ToLongTimeString () + ", " + date.ToLongDateString ();
		Encryptor.Keys = CryptKey.Convert (keyText);

		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetString ("Date", keyText);
		WriteProgressData ();
		PlayerPrefs.Save ();
	}

	void WriteProgressData() {
		GameObject paddle = GameObject.FindGameObjectWithTag ("Player");
		if (paddle != null) {
			SetValue ("Level", GameController.Instance.CurrentLevel + 1);
			CreatureParameters creatureParameters = paddle.GetComponent<CreatureParametersController> ().InitialParameters;
			PropertyInfo[] properties = creatureParameters.GetType ().GetProperties ();
			foreach (PropertyInfo property in properties) {
				SetValue(property.Name, (float)property.GetValue(creatureParameters, null));
			}
			PaddleParameters paddleParameters = paddle.GetComponent<PaddleParametersController> ().InitialParameters;
			properties = paddleParameters.GetType ().GetProperties ();
			foreach (PropertyInfo property in properties) {
				SetValue(property.Name, (float)property.GetValue(paddleParameters, null));
			}
		}
	}

	public void ClearProgress() {
		PlayerPrefs.DeleteAll ();
	}

	bool CheatingTakesPlace() {
		return false;
	}
}
