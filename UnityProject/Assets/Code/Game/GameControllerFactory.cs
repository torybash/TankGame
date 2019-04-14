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
		private readonly TankDatabase tankDatabase;
		private readonly CrewDatabase crewDatabase;

		public GameControllerFactory(FlowStack flowStack, ViewController viewController, TankDatabase tankDatabase, CrewDatabase crewDatabase)
		{
			this.flowStack = flowStack;
			this.viewController = viewController;
			this.tankDatabase = tankDatabase;
			this.crewDatabase = crewDatabase;
		}

		public Game GetGame()
		{
			var game = new Game(viewController, this, tankDatabase, crewDatabase);
			return game;
		}

		public Battle GetBattle(GameState gameState)
		{
			var battle = new Battle(viewController, gameState);
			return battle;
		}

		//public WorldMap GetWorldMap()
		//{
		//	var worldMap = new WorldMap(viewController);
		//	return worldMap;
		//}
	}

}