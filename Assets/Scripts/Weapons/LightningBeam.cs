using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponInitializer))]
public class LightningBeam : MonoBehaviour {

	private float shootCooldown;
	private int layerMask;

	private Weapon weapon;

	public AudioClip shotSound;
	private AudioSource audioSource;

	public GameObject lightningObject;
	private Lightning lightning;

	public Transform[] muzzles;
	private int activeMuzzle;

	void Awake () {
		weapon = GetComponent<Weapon> ();
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = shotSound;
		Initialize ();
		activeMuzzle = 0;
		layerMask = 1 << 8 << 9 << 11;
		layerMask = ~layerMask;
	}

	void Initialize() {
		GameObject cloneLightning = Instantiate (lightningObject, transform.position, Quaternion.identity) as GameObject;
		cloneLightning.transform.parent = transform;
		lightning = cloneLightning.GetComponent<Lightning> ();
	}
	
	void Update () {		
		bool shoot = Input.GetButton("Fire1");
		shoot |= Input.GetMouseButton(0);

		if (shoot) {
			if (CanAttack) {
				Shoot ();
			}
		}

		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}
	}

	void Shoot() {
		Transform muzzle = muzzles [activeMuzzle];
		RaycastHit2D hit = Physics2D.Raycast (muzzle.position, Vector2.up, 20f, layerMask);
		IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable> ();
		if (damageable != null) {
			damageable.ApplyDamage (weapon);
		}
		lightning.Create (muzzle.position, hit.point);
		audioSource.Play ();
		SwitchMuzzle ();
		shootCooldown = weapon.GetCurrentValue("Firerate");
	}

	void SwitchMuzzle() {
		activeMuzzle = (int)Mathf.Repeat (activeMuzzle + 1, muzzles.Length);
	}

	public bool CanAttack {
		get { return shootCooldown <= 0f;} 
	}
}
