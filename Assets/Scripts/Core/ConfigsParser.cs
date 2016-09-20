using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ConfigsParser : MonoBehaviour {

	public static ConfigsParser instance = null;

	public TextAsset enemiesJSON;
	public EnemiesConfig enemiesConfig;

	public TextAsset paddleJSON;
	public PaddleConfig paddleConfig;

	public TextAsset destructiblesJSON;
	public EnemiesConfig destructiblesConfig;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
		
		enemiesConfig = JsonUtility.FromJson<EnemiesConfig>(enemiesJSON.text);
		destructiblesConfig = JsonUtility.FromJson<EnemiesConfig>(destructiblesJSON.text);
		paddleConfig = JsonUtility.FromJson<PaddleConfig>(paddleJSON.text);
	}
}
