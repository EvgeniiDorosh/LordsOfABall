using UnityEngine;
using System.Collections;
using System;

public class UIDamage : MonoBehaviour 
{
	Animator anim;

	IDamageable paddle;

	void Awake () 
	{
		anim = GetComponent<Animator>();
	}
	
	void Start() 
	{
		paddle = GameObject.FindGameObjectWithTag ("Player").GetComponent<IDamageable> ();
		paddle.GotDamage += ShowDamageEffect;
	}

	void ShowDamageEffect(object sender, EventArgs e) 
	{
		anim.SetTrigger ("GotDamage");
	}
}
