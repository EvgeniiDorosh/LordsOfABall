using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class ParametersController {
	
	private readonly Dictionary<string, Action<float>> changeMethods = new Dictionary<string, Action<float>>();

	private CreatureParameters initialParameters;
	public CreatureParameters InitialParameters {
		get { 
			return initialParameters;
		}
		set { 
			initialParameters = value;
		}
	}

	private CreatureParameters currentParameters;
	public CreatureParameters CurrentParameters {
		get { 
			return currentParameters;
		}
		set { 
			currentParameters = value;
		}
	}

	public string destinationEvent = "";

	public void ChangeParameter(string paramName, float diffValue) {

		string methodName = "Change" + paramName;
		if(changeMethods.ContainsKey(methodName)) {
			changeMethods[methodName].Invoke(diffValue);
		} else {
			MethodInfo method = GetType ().GetMethod (methodName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (method != null) {
				Action<float> action = (Action<float>)Delegate.CreateDelegate(typeof(Action<float>), this, method);
				changeMethods.Add (methodName, action);
				action.Invoke (diffValue);
			}
		}
	}

	/**
	 * Change methods
	 */

	protected void ChangeHealth(float diff) {
		currentParameters.Health += diff;
		if (currentParameters.Health > initialParameters.Health)
			currentParameters.Health = initialParameters.Health;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Health", currentParameters.Health, diff));
	}

	protected void ChangeInitialHealth(float diff) {
		initialParameters.Health += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialHealth", initialParameters.Health, diff));
	}

	protected void ChangeAttack(float diff) {
		currentParameters.Attack += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Attack", currentParameters.Attack, diff));
	}

	protected void ChangeInitialAttack(float diff) {
		initialParameters.Attack += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialAttack", initialParameters.Attack, diff));
		ChangeAttack (diff);
	}

	protected void ChangeDefense(float diff) {
		currentParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Defense", currentParameters.Defense, diff));
	}

	protected void ChangeInitialDefense(float diff) {
		initialParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialDefense", initialParameters.Defense, diff));
		ChangeDefense (diff);
	}

	protected void ChangeMinimumDamage(float diff) {
		currentParameters.MinimumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("MinimumDamage", currentParameters.MinimumDamage, diff));
	}

	protected void ChangeInitialMinimumDamage(float diff) {
		initialParameters.MinimumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMinimumDamage", initialParameters.MinimumDamage, diff));
		ChangeMinimumDamage (diff);
	}

	protected void ChangeMaximumDamage(float diff) {
		currentParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("MaximumDamage", currentParameters.MaximumDamage, diff));
	}

	protected void ChangeInitialMaximumDamage(float diff) {
		initialParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMaximumDamage", initialParameters.MaximumDamage, diff));
		ChangeMaximumDamage (diff);
	}

	protected void ChangeInitiative(float diff) {
		currentParameters.Initiative += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Initiative", currentParameters.Initiative, diff));
	}

	protected void ChangeInitialInitiative(float diff) {
		initialParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialInitiative", initialParameters.Initiative, diff));
		ChangeInitiative (diff);
	}

	protected void ChangeMana(float diff) {
		currentParameters.Mana += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Mana", currentParameters.Mana, diff));
	}

	protected void ChangeInitialMana(float diff) {
		initialParameters.Mana += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMana", initialParameters.Mana, diff));
	}

	protected void ChangeSpellPower(float diff) {
		currentParameters.SpellPower += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("SpellPower", currentParameters.SpellPower, diff));
	}

	protected void ChangeInitialSpellPower(float diff) {
		initialParameters.SpellPower += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialSpellPower", initialParameters.SpellPower, diff));
		ChangeSpellPower (diff);
	}

	protected void ChangeLuck(float diff) {
		currentParameters.Luck += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Luck", currentParameters.Luck, diff));
	}

	protected void ChangeInitialLuck(float diff) {
		initialParameters.Luck += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialLuck", initialParameters.Luck, diff));
		ChangeLuck (diff);
	}

	protected void ChangeMorale(float diff) {
		currentParameters.Morale += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Morale", currentParameters.Morale, diff));
	}

	protected void ChangeInitialMorale(float diff) {
		initialParameters.Morale += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMorale", initialParameters.Morale, diff));
		ChangeMorale (diff);
	}
}
