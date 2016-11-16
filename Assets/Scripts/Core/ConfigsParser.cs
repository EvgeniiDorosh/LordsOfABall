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
	public DestructiblesConfig destructiblesConfig;

	public TextAsset weaponsJSON;
	public WeaponsConfig weaponsConfig;

	void Awake () {
		if (instance != null) {
			DestroyImmediate (gameObject);
			return;
		}

		instance = this;
		
		enemiesConfig = JsonUtility.FromJson<EnemiesConfig>(enemiesJSON.text);
		destructiblesConfig = JsonUtility.FromJson<DestructiblesConfig>(destructiblesJSON.text);
		paddleConfig = JsonUtility.FromJson<PaddleConfig>(paddleJSON.text);
		weaponsConfig = JsonUtility.FromJson<WeaponsConfig>(weaponsJSON.text);
	}
}
