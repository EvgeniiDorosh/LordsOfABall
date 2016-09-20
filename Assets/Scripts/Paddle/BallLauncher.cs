using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour {

	public Transform ballInitialSpot;
	public GameObject ball;
	private GameObject caughtBall;

	private bool hasBall = true;

	public bool CatchingIsActive { get; set; }

	private float reflectionFactor;
	public float ReflectionFactor { 
		get { return reflectionFactor;}
		set {
			reflectionFactor = Mathf.Clamp(value, 0.1f, 0.9f);
		}
	}

	private Collider2D platform;

	public void SetWidth(float value) {
		transform.localScale = new Vector3(value, 1f, 1f);
	}

	void Awake() {
		ReflectionFactor = 0.5f;
		SetupBall ();
	}

	void Start () {
		platform = GetComponent<Collider2D> ();
	}
	
	void Update () {
		if (Input.GetButtonDown ("Fire1") && hasBall) {
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

	public void SetupBall() {
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
		caughtBall.GetComponent<Transform> ().SetParent (this.transform);
	}

	float HitFactor(Vector2 hitPosition) {
		return (hitPosition.x - transform.position.x) / platform.bounds.size.x;
	}
}
