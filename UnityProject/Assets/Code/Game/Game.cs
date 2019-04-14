using System;
using System.Collections;
using TankGame.Views;
using UnityEngine;

namespace TankGame.Game
{
	public class Game
	{
		private readonly ViewController viewController;
		private readonly GameControllerFactory gameControllerFactory;
		private readonly TankDatabase tankDatabase;
		private readonly CrewDatabase crewDatabase;

		private Routine battleRoutine = new Routine();

		private GameState gameState;

		public bool IsPlaying { get; private set; }

		public Game(ViewController viewController, GameControllerFactory gameControllerFactory, TankDatabase tankDatabase, CrewDatabase crewDatabase)
		{
			this.viewController = viewController;
			this.gameControllerFactory = gameControllerFactory;
			this.tankDatabase = tankDatabase;
			this.crewDatabase = crewDatabase;
		}

		public IEnumerator Run()
		{
			IsPlaying = true;

			gameState = DEBUG_GetTestState();

			var mapView = viewController.ShowView<WorldMapView>();
			mapView.OnDestinationSelected += OnDestinationSelected;

			while (mapView.IsVisible())
			{
				yield return null;
			}

			while (battleRoutine.IsRunning)
			{
				yield return null;
			}
		}

		private void OnDestinationSelected(string destinationId)
		{
			var battle = gameControllerFactory.GetBattle(gameState);
			battleRoutine = new Routine();
			battleRoutine.Start(battle.Run());
		}


		private GameState DEBUG_GetTestState()
		{
			var testState = new GameState
			{
				tankState = tankDatabase.GetTankState("Test"),
				crewMemberStates = crewDatabase.GetCrew("Test")
			};
			return testState;
		}
	}
}