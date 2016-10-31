using UnityEngine;
using System.Collections;

public class ParticlesByTime : MonoBehaviour {

	private ParticleSystem particles;
	public float lifeTime = 0.5f;
		
	void Awake() {
		particles = GetComponent<ParticleSystem> ();
	}

	void OnEnable () {
		CancelInvoke ();
		particles.Play ();
		Invoke ("Hide", lifeTime);
	}

	void Hide() {
		CancelInvoke ();
		gameObject.SetActive (false);
	}
}
