using UnityEngine;
using System.Collections;

public class StableDestructible : Destructible {

	public int health;
	private int damagePerHit = 1;

	new void Awake () {
		base.Awake ();
		ParamsController.InitialParameters = new CreatureParameters();
		ParamsController.InitialParameters.Health = health;
		ParamsController.CurrentParameters = InitialParameters.Clone();
		destructibles.Add (this.gameObject);
	}

	override public void ApplyDamage (Attacker attacker) {		
		ApplyDamage (damagePerHit);
	}

	override public void ApplyDamage(float damage) {
		CurrentParameters.Health = CurrentParameters.Health - damagePerHit;
	}
}
