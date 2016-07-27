using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour {

	public Transform ballInitialSpot;
	public GameObject ball;
	private GameObject caughtBall;

	private bool hasBall = true;

	private bool catchingIsActive = false;
	public bool CatchingIsActive { get; set; }

	private float paddleWidth;

	void Awake() {
		SetupBall ();
	}

	void Start () {
		paddleWidth = GetComponent<Collider2D> ().bounds.size.x;
	}
	
	void Update () {
		if (Input.GetButtonDown ("Fire1") && hasBall) {
			LaunchBall (caughtBall);
			hasBall = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag ("Ball")) {
			if (catchingIsActive && !hasBall) {
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
		Vector2 direction = new Vector2(xPos, 0.7f).normalized;
		ball.GetComponent<Ball> ().Launch (direction);
	}

	void CatchBall(GameObject ball) {
		caughtBall = ball;
		caughtBall.GetComponent<Ball> ().Stop ();
		caughtBall.GetComponent<Transform> ().SetParent (this.transform);
	}

	float HitFactor(Vector2 hitPosition) {
		return (hitPosition.x - transform.position.x) / paddleWidth;
	}
}
