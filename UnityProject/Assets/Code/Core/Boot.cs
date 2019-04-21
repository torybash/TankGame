using System;
using System.Collections;
using System.Collections.Generic;
using TankGame.Databases;
using TankGame.Flow;
using TankGame.Game;
using TankGame.MainMenu;
using TankGame.Views;
using UnityEngine;

namespace TankGame
{
	public class Boot : MonoBehaviour
	{
		[SerializeField] private DatabaseHelper databaseHelper;

		void Start()
		{
			BootGame();
		}

		private void BootGame()
		{
			//Setting up controllers, databases, flow-stack etc.

			//Core functionality
			var viewDatabase = databaseHelper.Get<ViewDatabase>();
			var viewController = new ViewController(viewDatabase);
			var flowStack = new FlowStack();

			//Game, map, battle
			var tankDatabase = databaseHelper.Get<TankDatabase>();
			var crewDatabase = databaseHelper.Get<CrewDatabase>();
			var cardsDatabase = databaseHelper.Get<CardsDatabase>();
			var activeCardsPanelLifecycleHandler = new ActiveCardsPanelLifecycleHandler(viewController);
			var abilitiesPanelLifecycleHandler = new AbilitiesPanelLifecycleHandler(tankDatabase);
			var tankPanelLifecycleHandler = new TankPanelLifecycleHandler();
			var dragAndDropArrow = new DragAndDropArrowController(viewController);
			var battleHUD = new BattleHUD(viewController, activeCardsPanelLifecycleHandler, abilitiesPanelLifecycleHandler, tankPanelLifecycleHandler, dragAndDropArrow);
			var gameControllerFactory = new GameControllerFactory(flowStack, viewController, battleHUD, tankDatabase, crewDatabase, cardsDatabase);

			//Main menu
			var mainMenuControllerFactory = new MainMenuControllerFactory(flowStack, viewController, gameControllerFactory);

			//Start Flow
			var menuFlow = new MainMenuFlow(mainMenuControllerFactory);
			flowStack.Push(menuFlow);
		}
	}

}