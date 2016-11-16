public class WeaponsConfig {

	public WeaponParametersConfig[] parameters;

	public WeaponParameters GetParametersByName(string name) {
		foreach (WeaponParametersConfig config in parameters) {
			if (config.name == name) {
				return ConvertConfigToParameters(config);
			}
		}
		return new WeaponParameters ();
	}

	private WeaponParameters ConvertConfigToParameters(WeaponParametersConfig config) {
		WeaponParameters result = new WeaponParameters ();
		result.Attack = config.attack;
		result.Damage = config.damage;
		result.Firerate = config.firerate;
		result.Speed = config.speed;
		result.DefenseIgnoring = config.defenseIgnoring;

		return result;
	}
}
