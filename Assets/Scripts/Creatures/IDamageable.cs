using System;

public interface IDamageable : ICreature, IDestructible
{
	event EventHandler GotDamage;
	void ApplyDamage (IAttacker attacker);
	void ApplyDamage (float damage);
}
