using UnityEngine;
using System.Collections;

public class Damageable : Creature, IDamageable {
	
	public void ApplyDamage (IAttacker attacker) {		
		float damage = attacker.GetDamage(CurrentParameters);
		CurrentParameters.Health = CurrentParameters.Health - damage;
	}
}
