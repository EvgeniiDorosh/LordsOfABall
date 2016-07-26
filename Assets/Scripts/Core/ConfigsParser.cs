using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ConfigsParser : MonoBehaviour {

	public static ConfigsParser instance = null;

	public TextAsset enemiesJSONConfig;
	public EnemiesConfig enemiesConfig;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
		
		enemiesConfig = JsonUtility.FromJson<EnemiesConfig>(enemiesJSONConfig.text);
	}
}
