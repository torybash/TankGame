using UnityEngine;
using System.Collections;
using TankGame.Flow;
using TankGame.Views;
using System;

namespace TankGame.Game
{
	public class GameControllerFactory
	{
		private readonly FlowStack flowStack;
		private readonly ViewController viewController;
		private readonly BattleHUD battleHUD;
		private readonly TankDatabase tankDatabase;
		private readonly CrewDatabase crewDatabase;
		private readonly CardsDatabase cardsDatabase;

		public GameControllerFactory(FlowStack flowStack, ViewController viewController, BattleHUD battleHUD, TankDatabase tankDatabase, CrewDatabase crewDatabase, CardsDatabase cardsDatabase)
		{
			this.flowStack = flowStack;
			this.viewController = viewController;
			this.battleHUD = battleHUD;
			this.tankDatabase = tankDatabase;
			this.crewDatabase = crewDatabase;
			this.cardsDatabase = cardsDatabase;
		}

		public Game GetGame()
		{
			var game = new Game(viewController, this, tankDatabase, crewDatabase, cardsDatabase);
			return game;
		}

		public Battle GetBattle(BattleState battleState)
		{
			var battle = new Battle(viewController, battleState, tankDatabase, battleHUD);
			return battle;
		}
	}

}