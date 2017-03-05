using UnityEngine;
using System.Collections;

public class BallImpulse : MonoBehaviour 
{
	StatsController statsController;
	float duration = 1f;

	void Awake() 
	{
		statsController = GetComponent<StatsController> ();
		float diff = statsController.Get<Stat> (StatType.Speed).Value;
		StatModifier modifier = new StatModifier (StatModifierType.BaseValue, StatType.Speed, diff, 1f);
		Destroy (this, 2f);
	}
}
