using UnityEngine;
using System.Collections;

public class Damageable : Creature {
	
	public virtual void ApplyDamage (Attacker attacker) {		
		float damage = attacker.GetDamage(CurrentParameters);
		CurrentParameters.Health = CurrentParameters.Health - damage;
	}

	public virtual void ApplyDamage(float damage) {
		CurrentParameters.Health = CurrentParameters.Health - damage;
	}
}
