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
			//Setup controllers, databases, flow-stack etc.
			var viewDatabase = databaseHelper.Get<ViewDatabase>();
			var viewController = new ViewController(viewDatabase);

			var flowStack = new FlowStack();


			var tankDatabase = databaseHelper.Get<TankDatabase>();
			var crewDatabase = databaseHelper.Get<CrewDatabase>();
			var cardsDatabase = databaseHelper.Get<CardsDatabase>();
			var activeCardsPanelLifecycleHandler = new ActiveCardsPanelLifecycleHandler(viewController);
			var abilitiesPanelLifecycleHandler = new AbilitiesPanelLifecycleHandler(tankDatabase);
			var tankPanelLifecycleHandler = new TankPanelLifecycleHandler();
			var battleHUD = new BattleHUD(viewController, activeCardsPanelLifecycleHandler, abilitiesPanelLifecycleHandler, tankPanelLifecycleHandler);
			var gameControllerFactory = new GameControllerFactory(flowStack, viewController, battleHUD, tankDatabase, crewDatabase, cardsDatabase);
			var mainMenuControllerFactory = new MainMenuControllerFactory(flowStack, viewController, gameControllerFactory);


			//var settingsPanelLifecycleHandler = new SettingsPanelLifecycleHandler();

			//Start Flow
			var menuFlow = new MainMenuFlow(mainMenuControllerFactory);
			flowStack.Push(menuFlow);
		}
	}

}