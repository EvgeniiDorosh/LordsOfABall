using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PaddleStatsPanel : MonoBehaviour 
{
	static string signPlus = "+";

	[SerializeField]
	Color positiveValueColor;
	[SerializeField]
	Color negativeValueColor;

	[SerializeField]
	StatInfoField[] statsValues;
	[SerializeField]
	StatInfoField[] statsBonuses;

	Dictionary<StatType, Text> values = new Dictionary<StatType, Text>();
	Dictionary<StatType, Text> bonuses = new Dictionary<StatType, Text>();

	StatsController paddleStatsController;

	void Start()
	{
		paddleStatsController = GameObject.FindGameObjectWithTag ("Player").GetComponent<StatsController> ();
		InitializeValueFields ();
		InitializeBonusFields ();
	}

	void InitializeValueFields()
	{
		foreach (var item in statsValues) 
		{
			if (!values.ContainsKey (item.statType)) 
			{
				values [item.statType] = item.textField;
				BaseStat stat = paddleStatsController.Get (item.statType);
				if (stat != null) 
				{
					UpdateValueText (stat);
					stat.ValueChanged += UpdateValueText;
				}					
			}
		}
	}

	void InitializeBonusFields()
	{
		foreach (var item in statsBonuses) 
		{
			if (!bonuses.ContainsKey (item.statType)) 
			{
				bonuses [item.statType] = item.textField;
				Stat stat = paddleStatsController.Get<Stat> (item.statType);
				if (stat != null) 
				{
					UpdateBonusText (stat);
					stat.ValueChanged += UpdateBonusText;
				}					
			}
		}
	}
	
	void UpdateValueText(BaseStat stat) 
	{
		if (values.ContainsKey (stat.Type)) 
		{
			values[stat.Type].text = Mathf.RoundToInt (stat.Value).ToString();
		}
	}

	void UpdateBonusText(BaseStat stat)
	{
		if (bonuses.ContainsKey (stat.Type)) 
		{
			float bonusValue = ((Stat)stat).ModifiersValue;
			Text textField = bonuses [stat.Type];
			if (bonusValue > 0) 
			{				
				textField.text = string.Format ("{0}{1}", signPlus, Mathf.RoundToInt (bonusValue).ToString ());
				textField.color = positiveValueColor;
			} 
			else if(bonusValue < 0) 
			{
				textField.text = Mathf.RoundToInt (bonusValue).ToString ();
				textField.color = negativeValueColor;
			} 
			else 
			{
				textField.color = Color.clear;
			}
		}
	}

	[Serializable]
	public class StatInfoField
	{
		public Text textField;
		public StatType statType;
	}
}
