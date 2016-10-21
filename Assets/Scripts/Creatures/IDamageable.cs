public interface IDamageable : ICreature {

	void ApplyDamage (IAttacker attacker);
	void ApplyDamage (float damage);

	void Demolish();
}
