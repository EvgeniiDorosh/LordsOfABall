using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WeaponInitializer))]
public class LightningBeam : MonoBehaviour {

	private float shootCooldown;
	private int layerMask;
	private float distance = 20f;

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
		layerMask = LayerMask.GetMask("Enemy", "Border");
	}

	void Initialize() {
		GameObject cloneLightning = Instantiate (lightningObject, transform.position, Quaternion.identity) as GameObject;
		cloneLightning.transform.parent = transform;
		lightning = cloneLightning.GetComponent<Lightning> ();
	}
	
	void Update () {		
		if (Input.GetButton("Fire1")) {
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
		RaycastHit2D hit = Physics2D.Raycast (muzzle.position, Vector2.up, distance, layerMask);
		if (hit.collider != null) {
			lightning.Create (transform.InverseTransformPoint(muzzle.position), transform.InverseTransformPoint(hit.point));
			audioSource.Play ();
			OnHit (hit);
			SwitchMuzzle ();
			shootCooldown = weapon.GetStatValue(StatType.Firerate) / 60f;
		}
	}

	void OnHit(RaycastHit2D hit) {
		IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable> ();
		if (damageable != null) {
			damageable.ApplyDamage (weapon);
		}
	}

	void SwitchMuzzle() {
		activeMuzzle = (int)Mathf.Repeat (activeMuzzle + 1, muzzles.Length);
	}

	public bool CanAttack {
		get { return shootCooldown <= 0f;} 
	}
}
