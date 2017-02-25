using System;
using System.Collections.Generic;

public class StatsSet : BaseStatsSet {

	public event EventHandler ModifierAdded;

	protected virtual void OnModifierAdded(StatModifier modifier)
	{
		EventHandler handler = ModifierAdded;
		if (handler != null) 
		{
			handler (modifier, null);
		}
	}
}


