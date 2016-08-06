using UnityEngine;
using System;
using System.Collections;

public class BallConfig : CreaturesConfig<BallParametersConfig, BallParameters> {

	public BallParameters GetInitialParameters() {
		BallParameters result = ConvertConfigToParameters (parameters [0]);
		return result;
	}

	protected override BallParameters ConvertConfigToParameters(BallParametersConfig config) {
		BallParameters result = base.ConvertConfigToParameters(config);
		result.Speed = config.speed;
		result.Luck = config.luck;

		return result;
	}
}
