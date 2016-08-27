using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class ParametersController {

	private Creature target;

	private readonly Dictionary<string, Action<float>> changeMethods = new Dictionary<string, Action<float>>();

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
		CurrentParameters.Health += diff;
		if (CurrentParameters.Health > InitialParameters.Health)
			CurrentParameters.Health = InitialParameters.Health;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Health", CurrentParameters.Health, diff));
	}

	protected void ChangeInitialHealth(float diff) {
		InitialParameters.Health += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialHealth", InitialParameters.Health, diff));
	}

	protected void ChangeAttack(float diff) {
		CurrentParameters.Attack += diff;
		Debug.Log("destinationEvent = " +  destinationEvent + "; Attack = " + CurrentParameters.Attack + "; diff = " + diff);
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Attack", CurrentParameters.Attack, diff));
	}

	protected void ChangeInitialAttack(float diff) {
		InitialParameters.Attack += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialAttack", InitialParameters.Attack, diff));
		ChangeAttack (diff);
	}

	protected void ChangeDefense(float diff) {
		CurrentParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Defense", CurrentParameters.Defense, diff));
	}

	protected void ChangeInitialDefense(float diff) {
		InitialParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialDefense", InitialParameters.Defense, diff));
		ChangeDefense (diff);
	}

	protected void ChangeMinimumDamage(float diff) {
		CurrentParameters.MinimumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("MinimumDamage", CurrentParameters.MinimumDamage, diff));
	}

	protected void ChangeInitialMinimumDamage(float diff) {
		InitialParameters.MinimumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMinimumDamage", InitialParameters.MinimumDamage, diff));
		ChangeMinimumDamage (diff);
	}

	protected void ChangeMaximumDamage(float diff) {
		CurrentParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("MaximumDamage", CurrentParameters.MaximumDamage, diff));
	}

	protected void ChangeInitialMaximumDamage(float diff) {
		InitialParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMaximumDamage", InitialParameters.MaximumDamage, diff));
		ChangeMaximumDamage (diff);
	}

	protected void ChangeInitiative(float diff) {
		CurrentParameters.Initiative += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Initiative", CurrentParameters.Initiative, diff));
	}

	protected void ChangeInitialInitiative(float diff) {
		InitialParameters.MaximumDamage += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialInitiative", InitialParameters.Initiative, diff));
		ChangeInitiative (diff);
	}

	protected void ChangeMana(float diff) {
		CurrentParameters.Mana += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Mana", CurrentParameters.Mana, diff));
	}

	protected void ChangeInitialMana(float diff) {
		InitialParameters.Mana += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMana", InitialParameters.Mana, diff));
	}

	protected void ChangeSpellPower(float diff) {
		CurrentParameters.SpellPower += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("SpellPower", CurrentParameters.SpellPower, diff));
	}

	protected void ChangeInitialSpellPower(float diff) {
		InitialParameters.SpellPower += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialSpellPower", InitialParameters.SpellPower, diff));
		ChangeSpellPower (diff);
	}

	protected void ChangeLuck(float diff) {
		CurrentParameters.Luck += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Luck", CurrentParameters.Luck, diff));
	}

	protected void ChangeInitialLuck(float diff) {
		InitialParameters.Luck += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialLuck", InitialParameters.Luck, diff));
		ChangeLuck (diff);
	}

	protected void ChangeMorale(float diff) {
		CurrentParameters.Morale += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Morale", CurrentParameters.Morale, diff));
	}

	protected void ChangeInitialMorale(float diff) {
		InitialParameters.Morale += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialMorale", InitialParameters.Morale, diff));
		ChangeMorale (diff);
	}
}
