using UnityEngine;
using System.Collections;

public class ChangeSpeedSpell : Spell {

	//private PaddleParametersController paddleParamsController;

	override public void Cast () {
		/*paddleParamsController = targetObject.GetComponentInParent<PaddleParametersController> ();
		if (paddleParamsController != null) {
			paddleParamsController.ChangeParameter("Speed", 5);
		}*/
	}

	override public void Finish() {
		/*if (paddleParamsController != null) {
			paddleParamsController.ChangeParameter("Speed", -5);
		}*/
	}
}
