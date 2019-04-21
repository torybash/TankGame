using System;
using System.Collections;
using System.Collections.Generic;
using TankGame.Flow;
using TankGame.Views;
using UnityEngine;

namespace TankGame.Game
{
	public class WorldMap
	{
		private readonly ViewController viewController;
		private readonly GameControllerFactory gameControllerFactory;
		private readonly TankDatabase tankDatabase;
		private readonly CrewDatabase crewDatabase;
		private readonly CardsDatabase cardsDatabase;
		private readonly FlowStack flowStack;
		private GameState gameState;

		public bool IsSelectingDestination { get; private set; }

		public WorldMap(ViewController viewController, GameControllerFactory gameControllerFactory, TankDatabase tankDatabase, CrewDatabase crewDatabase, CardsDatabase cardsDatabase, FlowStack flowStack)
		{
			this.viewController = viewController;
			this.gameControllerFactory = gameControllerFactory;
			this.tankDatabase = tankDatabase;
			this.crewDatabase = crewDatabase;
			this.cardsDatabase = cardsDatabase;
			this.flowStack = flowStack;
		}

		public void Run()
		{
			IsSelectingDestination = true;

			gameState = DEBUG_GetTestState();

			var mapView = viewController.ShowView<WorldMapView>();
			mapView.OnDestinationSelected += OnDestinationSelected;
		}

		private void OnDestinationSelected(string destinationId)
		{
			var battleState = DEBUG_GetTestBattle();

			var battleFlow = new BattleFlow(gameControllerFactory, battleState);
			flowStack.Push(battleFlow);

			IsSelectingDestination = false;
		}

		private GameState DEBUG_GetTestState()
		{
			var testState = new GameState
			{
				tankState = tankDatabase.GetTankState("Test"),
				crewMemberStates = crewDatabase.GetCrew("Test")
			};
			for (int i = 0; i < testState.tankState.tankSectionState.Count; i++)
			{
				var partState = testState.tankState.tankSectionState[i];
				testState.crewMemberStates[i].TankPart = partState.tankSection;
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