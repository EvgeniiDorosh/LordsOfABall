using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour, IDamageable {

	public BaseCreatureParameters currentParameters;
	public BaseCreatureParameters initialParameters;

	public void ApplyDamage (IAttacker attacker) {
		float damage = attacker.GetDamage(currentParameters);
		currentParameters.Health = currentParameters.Health - damage;
	}
}
