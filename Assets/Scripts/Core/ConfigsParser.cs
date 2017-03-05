using UnityEngine;
using System.Collections;

public class ConfigsParser : MonoBehaviour {

	public static ConfigsParser instance = null;

	[SerializeField]
	TextAsset enemiesJSON;
	public static EnemiesConfig EnemiesConfig { get; private set;}

	[SerializeField]
	TextAsset paddleJSON;
	public static PaddleConfig PaddleConfig { get; private set;}

	[SerializeField]
	TextAsset destructiblesJSON;
	public static DestructiblesConfig DestructiblesConfig { get; private set;}

	[SerializeField]
	TextAsset weaponsJSON;
	public static WeaponsConfig WeaponsConfig { get; private set;}

	void Awake () {
		if (instance != null) {
			DestroyImmediate (gameObject);
			return;
		}

		instance = this;
		
		EnemiesConfig = JsonUtility.FromJson<EnemiesConfig>(enemiesJSON.text);
		EnemiesConfig.Initialize ();

		DestructiblesConfig = JsonUtility.FromJson<DestructiblesConfig>(destructiblesJSON.text);
		DestructiblesConfig.Initialize ();

		PaddleConfig = JsonUtility.FromJson<PaddleConfig> (paddleJSON.text);
		PaddleConfig.Initialize();

		WeaponsConfig = JsonUtility.FromJson<WeaponsConfig>(weaponsJSON.text);
		WeaponsConfig.Initialize ();
	}
}
