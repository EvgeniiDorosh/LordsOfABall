using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ConfigsParser : MonoBehaviour {

	public static ConfigsParser instance = null;

	public TextAsset enemiesJSONConfig;
	public EnemiesConfig enemiesConfig;

	public TextAsset ballJSONConfig;
	public BallConfig ballConfig;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
		
		enemiesConfig = JsonUtility.FromJson<EnemiesConfig>(enemiesJSONConfig.text);
		ballConfig = JsonUtility.FromJson<BallConfig>(ballJSONConfig.text);
	}
}
