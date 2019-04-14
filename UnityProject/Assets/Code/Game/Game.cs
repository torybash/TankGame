using System;
using System.Collections;
using System.Collections.Generic;
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
		private readonly CardsDatabase cardsDatabase;
		private Routine battleRoutine = new Routine();

		private GameState gameState;

		public bool IsPlaying { get; private set; }

		public Game(ViewController viewController, GameControllerFactory gameControllerFactory, TankDatabase tankDatabase, CrewDatabase crewDatabase, CardsDatabase cardsDatabase)
		{
			this.viewController = viewController;
			this.gameControllerFactory = gameControllerFactory;
			this.tankDatabase = tankDatabase;
			this.crewDatabase = crewDatabase;
			this.cardsDatabase = cardsDatabase;
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
			var battleState = DEBUG_GetTestBattle();

			var battle = gameControllerFactory.GetBattle(battleState);
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
			for (int i = 0; i < testState.tankState.tankPartStates.Count; i++)
			{
				var partState = testState.tankState.tankPartStates[i];
				testState.crewMemberStates[i].TankPart = partState.tankPart;
			}
			return testState;
		}


		private BattleState DEBUG_GetTestBattle()
		{
			var battleState = new BattleState
			{
				round = 0,
				battlePhase = BattlePhase.START_OF_ROUND,
				deck = cardsDatabase.GetRandomCards(20),
				activeCards = new List<CardData>(),
				gameState = gameState

			};
			return battleState;
		}
	}
}