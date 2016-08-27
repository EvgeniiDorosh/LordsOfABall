using UnityEngine;
using System.Collections;

public class Damageable : Creature {
	
	public void ApplyDamage (Attacker attacker) {		
		float damage = attacker.GetDamage(CurrentParameters);
		CurrentParameters.Health = CurrentParameters.Health - damage;
	}
}
