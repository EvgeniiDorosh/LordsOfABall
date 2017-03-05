using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Paddle))]
[RequireComponent(typeof(PaddleMotionController))]
[RequireComponent(typeof(StatsController))]
public class PaddleInitializer : MonoBehaviour
{
	StatsController statsController;

	void Awake () 
	{
		statsController = GetComponent<StatsController> ();
		InitParameters ();
	}

	void InitParameters() 
	{
		int currentLevel = GameController.Instance.CurrentLevel;
		switch (GameController.Instance.CurrentGameMode)
		{
		case GameMode.Campaign:
			if (currentLevel == 0) 
			{
				InitFromConfig (currentLevel);
			}
			else 
			{
				InitFromConfig (currentLevel);
				//InitFromSaveData ();
			}
			break;
		case GameMode.Single: 
			InitFromConfig (currentLevel);
			break;
		case GameMode.Survival: 
			break;
		}
	}

	void InitFromConfig(int currentLevel) 
	{
		Stat stat = null;
		List<StatBlank> blanks = ConfigsParser.PaddleConfig.GetBlanks ("Paddle");
		foreach (var blank in blanks) 
		{
			stat = new Stat (blank.type, blank.value, null, 0);
			statsController.Add(stat);
		}

		Stat attack = statsController.Get<Stat> (StatType.Attack);
		attack.AddModifier(new StatModifier(StatModifierType.BaseValue, StatType.Attack, 5));

		Stat defense = statsController.Get<Stat> (StatType.Defense);
		defense.AddModifier(new StatModifier(StatModifierType.BaseValue, StatType.Defense, 10));
		defense.AddModifier(new StatModifier(StatModifierType.BaseValuePercent, StatType.Defense, 0.5f));

		Stat health = statsController.Get<Stat> (StatType.Health);
		Stat mana = statsController.Get<Stat> (StatType.Mana);

		statsController.Add (new BaseClampedStat (StatType.CurrentHealth, health.Value, health));
		statsController.Add (new BaseClampedStat (StatType.CurrentMana, mana.Value, null, 0));

		Stat maxDamage = statsController.Get<Stat> (StatType.MaximumDamage);
		Stat minDamage = statsController.Get<Stat> (StatType.MinimumDamage);
		minDamage.SetMax (maxDamage);
		minDamage.AddModifier(new StatModifier(StatModifierType.BaseValue, StatType.MinimumDamage, 10f));
		maxDamage.AddModifier(new StatModifier(StatModifierType.BaseValue, StatType.MaximumDamage, -3f));
	}

	/*void InitFromSaveData() {
		PrefsManager prefsManager = PrefsManager.Instance;
		CreatureParameters creatureInitialParams = new CreatureParameters();
		PropertyInfo[] properties = creatureInitialParams.GetType ().GetProperties ();
		foreach (PropertyInfo property in properties) {
			creatureInitialParams.SetValue(property.Name, prefsManager.GetFloat(property.Name));
		}
		creatureParamsController.InitialParameters = creatureInitialParams;
		creatureParamsController.CurrentParameters = creatureParamsController.InitialParameters.Clone ();
		creatureParamsController.DestinationEvent = PaddleEvent.parameterWasUpdated;

		PaddleParameters paddleInitialParams = new PaddleParameters();
		properties = paddleInitialParams.GetType ().GetProperties ();
		foreach (PropertyInfo property in properties) {
			paddleInitialParams.SetValue(property.Name, prefsManager.GetFloat(property.Name));
		}
		paddleParamsController.InitialParameters = paddleInitialParams;
		paddleParamsController.CurrentParameters = paddleParamsController.InitialParameters.Clone();
	}

	void InitUIPanel() {
		PropertyInfo[] properties = creatureParamsController.CurrentParameters.GetType ().GetProperties ();
		foreach (PropertyInfo property in properties) {
			creatureParamsController.ChangeParameter(property.Name, 0.0f);
			//Console.WriteLine ("Property {0} is equal to {1}", property.Name, creatureParamsController.CurrentParameters.GetValue (property.Name));
			creatureParamsController.ChangeParameter("Initial" + property.Name, 0.0f);
		}

		properties = paddleParamsController.CurrentParameters.GetType ().GetProperties ();
		foreach (PropertyInfo property in properties) {
			paddleParamsController.ChangeParameter(property.Name, 0.0f);
			//Console.WriteLine ("Property {0} is equal to {1}", property.Name, paddleParamsController.CurrentParameters.GetValue (property.Name));
			paddleParamsController.ChangeParameter("Initial" + property.Name, 0.0f);
		}
	}*/
}
