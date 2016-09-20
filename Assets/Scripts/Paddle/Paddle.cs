using UnityEngine;
using System.Collections;

public class Paddle : Attacker {

	private PaddleConfig config;

	private BallLauncher launcher;
	public BallLauncher Launcher {
		get {
			return launcher;
		}
	}

	new void Awake () {
		base.Awake ();
		config = ConfigsParser.instance.paddleConfig;
		width = config.width;
		defaultBallSpeed = config.defaultBallSpeed;
		ParamsController.InitialParameters = config.GetInitialParameters();
		ParamsController.CurrentParameters = InitialParameters.Clone ();
		ParamsController.destinationEvent = PaddleEvent.parameterWasUpdated;

		launcher = GetComponentInChildren<BallLauncher> ();
	}

	void Start() {
		PaddleUIInitializator uiInitializator = GetComponent<PaddleUIInitializator> ();
		uiInitializator.InitUIPanel ();
	}

	private float width;
	public float Width {
		get {
			return width; 
		}
		set { 
			width = Mathf.Clamp(value, 0.5f, 2.0f);
			launcher.SetWidth(width);
			Messenger.Invoke (PaddleEvent.widthWasUpdated);
		}
	}

	private float defaultBallSpeed;
	public float DefaultBallSpeed {
		get {
			return defaultBallSpeed;
		}
		set { 
			defaultBallSpeed = value;
			Messenger<float>.Invoke(PaddleEvent.speedWasUpdated, defaultBallSpeed);
		}
	}
}
