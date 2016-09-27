using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class CreatureParametersController : ParametersController {
	
	protected CreatureParameters initialParameters;
	public CreatureParameters InitialParameters {
		get { return initialParameters; }
		set { initialParameters = value; }
	}

	protected CreatureParameters currentParameters;
	public CreatureParameters CurrentParameters {
		get { return currentParameters; }
		set { currentParameters = value; }
	}

	protected string destinationEvent = "";
	public string DestinationEvent {
		get { return destinationEvent; }
		set { destinationEvent = value; }
	}

	/**
	 * Change methods
	 */

	private void ChangeHealth(float diff) {
		currentParameters.Health += diff;
		if (currentParameters.Health > initialParameters.Health)
			currentParameters.Health = initialParameters.Health;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Health", currentParameters.Health, diff));
	}

	private void ChangeInitialHealth(float diff) {
		initialParameters.Health += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialHealth", initialParameters.Health, diff));
	}

	private void ChangeAttack(float diff) {
		currentParameters.Attack += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Attack", currentParameters.Attack, diff));
	}

	private void ChangeInitialAttack(float diff) {
		initialParameters.Attack += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialAttack", initialParameters.Attack, diff));
		ChangeAttack (diff);
	}

	private void ChangeDefense(float diff) {
		currentParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Defense", currentParameters.Defense, diff));
	}

	private void ChangeInitialDefense(float diff) {
		initialParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialDefense", initialParameters.Defense, diff));
		ChangeDefense (diff);
	}

	private void ChangeMinimumDamage(float diff) {
		currentParameters.MinimumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("MinimumDamage", currentParameters.MinimumDamage, diff));
	}

	private void ChangeInitialMinimumDamage(float diff) {
		initialParameters.MinimumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMinimumDamage", initialParameters.MinimumDamage, diff));
		ChangeMinimumDamage (diff);
	}

	private void ChangeMaximumDamage(float diff) {
		currentParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("MaximumDamage", currentParameters.MaximumDamage, diff));
	}

	private void ChangeInitialMaximumDamage(float diff) {
		initialParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMaximumDamage", initialParameters.MaximumDamage, diff));
		ChangeMaximumDamage (diff);
	}

	private void ChangeInitiative(float diff) {
		currentParameters.Initiative += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Initiative", currentParameters.Initiative, diff));
	}

	private void ChangeInitialInitiative(float diff) {
		initialParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialInitiative", initialParameters.Initiative, diff));
		ChangeInitiative (diff);
	}

	private void ChangeMana(float diff) {
		currentParameters.Mana += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Mana", currentParameters.Mana, diff));
	}

	private void ChangeInitialMana(float diff) {
		initialParameters.Mana += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMana", initialParameters.Mana, diff));
	}

	private void ChangeSpellPower(float diff) {
		currentParameters.SpellPower += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("SpellPower", currentParameters.SpellPower, diff));
	}

	private void ChangeInitialSpellPower(float diff) {
		initialParameters.SpellPower += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialSpellPower", initialParameters.SpellPower, diff));
		ChangeSpellPower (diff);
	}

	private void ChangeLuck(float diff) {
		currentParameters.Luck += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Luck", currentParameters.Luck, diff));
	}

	private void ChangeInitialLuck(float diff) {
		initialParameters.Luck += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialLuck", initialParameters.Luck, diff));
		ChangeLuck (diff);
	}

	private void ChangeMorale(float diff) {
		currentParameters.Morale += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Morale", currentParameters.Morale, diff));
	}

	private void ChangeInitialMorale(float diff) {
		initialParameters.Morale += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMorale", initialParameters.Morale, diff));
		ChangeMorale (diff);
	}
}
