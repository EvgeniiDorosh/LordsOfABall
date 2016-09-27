using UnityEngine;
using System;
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
		SaveCurrentProgress ();
	}

	public static void SaveCurrentProgress() {
		DateTime date = DateTime.Now;
		string keyText = date.ToLongTimeString () + ", " + date.ToLongDateString ();

		Encryptor.Keys = CryptKey.Convert (keyText);
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetString ("Date", keyText);
		// Using Date as key, encrypt all keys and values;
		PlayerPrefs.Save ();
	}

	public static string GetString(string key) {
		return "";
	}

	public static int GetInt(string key) {
		return 1;
	}

	public static float GetFloat(string key) {
		return 1f;
	}
}
