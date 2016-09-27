using UnityEngine;
using System.Collections;

public class PaddleConfig : CreaturesConfig<CreatureParametersConfig> {

	public float width;
	public float speed;

	public CreatureParameters GetInitialParameters() {
		CreatureParameters result = ConvertConfigToParameters (parameters [0]);
		return result;
	}

	public PaddleParameters GetPaddleParameters() {
		PaddleParameters result = new PaddleParameters ();
		result.Width = width;
		result.Speed = speed;

		return result;
	}
}
