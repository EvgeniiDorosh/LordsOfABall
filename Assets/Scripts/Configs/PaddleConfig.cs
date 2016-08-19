using UnityEngine;
using System.Collections;

public class PaddleConfig : CreaturesConfig<CreatureParametersConfig> {

	public float width;
	public float defaultBallSpeed;

	public CreatureParameters GetInitialParameters() {
		CreatureParameters result = ConvertConfigToParameters (parameters [0]);
		return result;
	}
}
