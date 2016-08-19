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

	public GameObject platform;

	new void Awake () {
		launcher = GetComponentInChildren<BallLauncher> ();
		config = ConfigsParser.instance.paddleConfig;
		width = config.width;
		defaultBallSpeed = config.defaultBallSpeed;
		InitialParameters = config.GetInitialParameters();
		CurrentParameters = InitialParameters.Clone ();
		base.Awake ();
	}

	new protected void InitControllers() {
		base.InitControllers ();
		ParamsController.destinationEvent = PaddleEvent.parameterWasUpdated;
	}

	private const float maxWidth = 2f;
	private const float minWidth = 0.5f;
	private float width;
	public float Width {
		get {
			return width; 
		}
		set { 
			width = Mathf.Clamp(value, minWidth, maxWidth);
			platform.transform.localScale = new Vector3(width, 1f, 1f);
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
