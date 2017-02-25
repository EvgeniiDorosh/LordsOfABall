using System;

public interface IStatsController<T> where T : BaseStatsSet
{
	T StatSet
	{
		get;
		set;
	}
}

