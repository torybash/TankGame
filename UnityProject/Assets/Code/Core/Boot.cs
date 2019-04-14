using System;
using System.Collections;
using System.Collections.Generic;
using TankGame.Databases;
using TankGame.Flow;
using TankGame.Game;
using TankGame.MainMenu;
using TankGame.Screen;
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

			var screenController = new ScreenController();
			var flowStack = new FlowStack(screenController);


			var tankDatabase = databaseHelper.Get<TankDatabase>();
			var crewDatabase = databaseHelper.Get<CrewDatabase>();
			var cardsDatabase = databaseHelper.Get<CardsDatabase>();
			var gameControllerFactory = new GameControllerFactory(flowStack, viewController, tankDatabase, crewDatabase, cardsDatabase);
			var mainMenuControllerFactory = new MainMenuControllerFactory(flowStack, viewController, gameControllerFactory);


			//var settingsPanelLifecycleHandler = new SettingsPanelLifecycleHandler();

			//Start Flow
			var menuFlow = new MainMenuFlow(mainMenuControllerFactory);
			flowStack.Push(menuFlow);
		}
	}

}