using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour {

	public Transform platform;
	public Transform ballInitialSpot;
	public GameObject ball;
	private GameObject caughtBall;

	private bool hasBall = true;

	public bool CatchingIsActive { get; set; }

	private float reflectionFactor;
	public float ReflectionFactor { 
		get { return reflectionFactor;}
		set { reflectionFactor = Mathf.Clamp(value, 0.1f, 0.9f); }
	}

	private BoxCollider2D platformCollider;

	private void SetWidth(StatChange change) {
		platform.transform.localScale = new Vector3 (change.current, 1f, 1f);
		platformCollider.size = new Vector2(platformCollider.size.x * change.RelativeDiff, platformCollider.size.y);
	}

	void Awake() {
		ReflectionFactor = 0.5f;
		SetupBall ();
	}

	void OnEnable() {
		Messenger<StatChange>.AddListener (PaddleEvent.widthWasUpdated, SetWidth);
		Messenger.AddListener(BallEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
	}

	void OnDisable() {
		Messenger<StatChange>.RemoveListener (PaddleEvent.widthWasUpdated, SetWidth);
		Messenger.RemoveListener(BallEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
	}

	void Start () {
		platformCollider = GetComponent<BoxCollider2D> ();
	}
	
	void Update () {
		if (Input.GetButton ("Fire1") && hasBall) {
			LaunchBall (caughtBall);
			hasBall = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag ("Ball")) {
			if (CatchingIsActive && !hasBall) {
				hasBall = true;
				CatchBall (collision.gameObject);
			} else {
				LaunchBall (collision.gameObject);
			}
		}
	}

	void CheckAllBallAreDestroyed() {
		if (MembersAccount.Count(Member.Ball) == 0) {
			Invoke("SetupBall", 0.5f);		
		}
	}

	void SetupBall() {
		caughtBall = Instantiate (ball, ballInitialSpot.position, Quaternion.identity) as GameObject;
		caughtBall.GetComponent<Transform> ().SetParent (this.transform);
		hasBall = true;
	}

	void LaunchBall(GameObject ball) {
		float xPos = HitFactor(ball.transform.position);
		Vector2 direction = new Vector2(xPos, reflectionFactor).normalized;
		ball.GetComponent<BallMotionController> ().Launch (direction);
	}

	void CatchBall(GameObject ball) {
		caughtBall = ball;
		caughtBall.GetComponent<BallMotionController> ().Stop ();
		caughtBall.transform.SetParent (this.transform);
	}

	float HitFactor(Vector2 hitPosition) {
		return (hitPosition.x - transform.position.x) / platformCollider.bounds.size.x;
	}
}
