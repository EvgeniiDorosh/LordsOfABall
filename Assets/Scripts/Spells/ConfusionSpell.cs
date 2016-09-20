using UnityEngine;
using System.Collections;

public class ConfusionSpell : Spell {

	private PaddleMotionController motionController;

	override public void Cast () {
		motionController = targetObject.GetComponentInParent<PaddleMotionController> ();
		if (motionController != null) {
			motionController.IsInverseMotion = true;
		}
	}

	override public void Finish() {
		if (motionController != null) {
			motionController.IsInverseMotion = false;
		}
	}
}
