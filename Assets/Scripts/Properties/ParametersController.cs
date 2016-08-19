using UnityEngine;
using System.Collections;

public class ParametersController {

	private Creature target;

	private CreatureParameters InitialParameters {
		get { 
			return target.InitialParameters;
		}
	}

	private CreatureParameters CurrentParameters {
		get { 
			return target.CurrentParameters;
		}
	}

	public string destinationEvent;

	public ParametersController(Creature creature) {
		target = creature;
	}

	public void ChangeHealth(float diff) {
		CurrentParameters.Health += diff;
		if (CurrentParameters.Health > InitialParameters.Health)
			CurrentParameters.Health = InitialParameters.Health;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Health", CurrentParameters.Health, diff));
	}

	public void ChangeInitialHealth(float diff) {
		InitialParameters.Health += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialHealth", InitialParameters.Health, diff));
	}

	public void ChangeAttack(float diff) {
		CurrentParameters.Attack += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Attack", CurrentParameters.Attack, diff));
	}

	public void ChangeInitialAttack(float diff) {
		InitialParameters.Attack += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialAttack", InitialParameters.Attack, diff));
		ChangeAttack (diff);
	}

	public void ChangeDefense(float diff) {
		CurrentParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Defense", CurrentParameters.Defense, diff));
	}

	public void ChangeInitialDefense(float diff) {
		InitialParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialDefense", InitialParameters.Defense, diff));
		ChangeDefense (diff);
	}

	public void ChangeMinimumDamage(float diff) {
		CurrentParameters.MinimumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("MinimumDamage", CurrentParameters.MinimumDamage, diff));
	}

	public void ChangeInitialMinimumDamage(float diff) {
		InitialParameters.MinimumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMinimumDamage", InitialParameters.MinimumDamage, diff));
		ChangeMinimumDamage (diff);
	}

	public void ChangeMaximumDamage(float diff) {
		CurrentParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("MaximumDamage", CurrentParameters.MaximumDamage, diff));
	}

	public void ChangeInitialMaximumDamage(float diff) {
		InitialParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMaximumDamage", InitialParameters.MaximumDamage, diff));
		ChangeMaximumDamage (diff);
	}

	public void ChangeInitiative(float diff) {
		CurrentParameters.Initiative += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Initiative", CurrentParameters.Initiative, diff));
	}

	public void ChangeInitialInitiative(float diff) {
		InitialParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialInitiative", InitialParameters.Initiative, diff));
		ChangeInitiative (diff);
	}

	public void ChangeMana(float diff) {
		CurrentParameters.Mana += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Mana", CurrentParameters.Mana, diff));
	}

	public void ChangeInitialMana(float diff) {
		InitialParameters.Mana += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMana", InitialParameters.Mana, diff));
	}

	public void ChangeSpellPower(float diff) {
		CurrentParameters.SpellPower += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("SpellPower", CurrentParameters.SpellPower, diff));
	}

	public void ChangeInitialSpellPower(float diff) {
		InitialParameters.SpellPower += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialSpellPower", InitialParameters.SpellPower, diff));
		ChangeSpellPower (diff);
	}

	public void ChangeLuck(float diff) {
		CurrentParameters.Luck += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Luck", CurrentParameters.Luck, diff));
	}

	public void ChangeInitialLuck(float diff) {
		InitialParameters.Luck += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialLuck", InitialParameters.Luck, diff));
		ChangeLuck (diff);
	}

	public void ChangeMorale(float diff) {
		CurrentParameters.Morale += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Morale", CurrentParameters.Morale, diff));
	}

	public void ChangeInitialMorale(float diff) {
		InitialParameters.Morale += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMorale", InitialParameters.Morale, diff));
		ChangeMorale (diff);
	}
}
