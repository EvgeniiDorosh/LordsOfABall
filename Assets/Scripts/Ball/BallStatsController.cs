﻿using System;
using UnityEngine;
	
public class BallStatsController : StatsController
{
	StatsController paddleStatsController;

	void Awake()
	{
		GameObject paddle = GameObject.FindGameObjectWithTag ("Player");
		paddleStatsController = paddle.GetComponent<StatsController> ();

		foreach (StatType type in Ball.statTypes) 
		{
			Stat stat = paddleStatsController.Get<Stat> (type);
			Add (new Stat (type, stat.Value));
		}
	}

	void OnEnable() 
	{		
		foreach (StatType type in Ball.statTypes) 
		{
			paddleStatsController.Get<Stat> (type).ValueChanged += OnPaddleStatChanged;
		}
	}

	void OnDisable()
	{
		foreach (StatType type in Ball.statTypes) 
		{
			paddleStatsController.Get<Stat> (type).ValueChanged -= OnPaddleStatChanged;
		}
	}

	void OnPaddleStatChanged(BaseStat stat) 
	{
		Get<Stat> (stat.Type).Value = stat.Value;
	}
}


