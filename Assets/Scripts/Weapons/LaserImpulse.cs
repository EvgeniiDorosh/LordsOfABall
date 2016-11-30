using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponInitializer))]
public class LaserImpulse : MonoBehaviour {

	private string fireButton = "Fire1";

	private float delay = 0.5f;
	private float holdingTime = 0f;
	private float maxPowerTime = 0f;
	private float powerTime = 0f;
	private float damage;

	private WaitForFixedUpdate waiting = new WaitForFixedUpdate();

	private float maxWidth = 1f;

	private int layerMask;
	private float distance = 20f;

	private Weapon weapon;

	public AudioClip shotSound;
	private AudioSource audioSource;

	public GameObject laserObject;
	private Laser laser;

	public Transform muzzle;

	void Awake() {
		weapon = GetComponent<Weapon> ();
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = shotSound;
		Initialize ();
		layerMask = LayerMask.GetMask("Enemy", "Border");
	}

	void Initialize() {
		GameObject cloneLaser = Instantiate (laserObject) as GameObject;
		cloneLaser.transform.SetParent(muzzle);
		cloneLaser.transform.localPosition = Vector3.zero;
		laser = cloneLaser.GetComponent<Laser> ();
	}

	void OnDisable() {
		StopShooting ();
	}

	void Update () {
		if (Input.GetButtonDown (fireButton)) {
			StopShooting ();
		}

		if (Input.GetButton (fireButton)) {
			holdingTime += Time.deltaTime;
			if (holdingTime >= delay) {
				powerTime += Time.deltaTime;
			}
		}

		if (Input.GetButtonUp(fireButton)) {
			if(powerTime > 0f) {
				StartCoroutine (Shoot ());
			}
		}
	}

	IEnumerator Shoot() {
		powerTime = Mathf.Clamp (powerTime, 0f, maxPowerTime);
		while (powerTime > 0f) {
			RaycastHit2D hit = Physics2D.Raycast (muzzle.position, Vector2.up, distance, layerMask);
			if (hit.collider != null) {
				float powerFactor = powerTime / maxPowerTime;
				float width = maxWidth * powerFactor;
				laser.Create (Vector2.zero, muzzle.InverseTransformPoint(hit.point), width, width);
				OnHit (hit, powerFactor);
			}
			powerTime -= Time.deltaTime;
			yield return waiting;
		}
		StopShooting ();
	}

	void OnHit(RaycastHit2D hit, float powerFactor) {
		IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable> ();
		if (damageable != null) {
			damageable.ApplyDamage (damage * powerFactor * Time.deltaTime);
		}
	}

	void StopShooting() {
		StopCoroutine (Shoot());
		laser.Clear ();
		holdingTime = 0f;
		powerTime = 0f;
		maxPowerTime = weapon.GetCurrentValue ("Firerate");
		damage = weapon.GetCurrentValue ("Damage");
	}
}
