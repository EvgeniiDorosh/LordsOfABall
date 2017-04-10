using UnityEngine;
using System.Collections;

public class ParticlesEffect : ShowingEffect 
{
	[SerializeField]
	float duration = 2f;

	ParticleSystem trail;
	AudioSource audioSource;

	void Awake() 
	{
		trail = GetComponent<ParticleSystem>();
		audioSource = GetComponent<AudioSource>();
	}

	public override void Show ()
	{
		trail.Play ();
		audioSource.Play ();
		Invoke ("Hide", duration);
	}

	void Hide() 
	{
		CancelInvoke ();
		gameObject.SetActive (false);
	}
}
