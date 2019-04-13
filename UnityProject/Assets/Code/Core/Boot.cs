using System;
using System.Collections;
using System.Collections.Generic;
using TankGame.Databases;
using TankGame.Flow;
using TankGame.MainMenu;
using TankGame.Screen;
using TankGame.Views;
using UnityEngine;

namespace TankGame
{
	public class Boot : MonoBehaviour
	{
		void Start()
		{
			BootGame();
		}

		private void BootGame()
		{
			//Setup controllers, databases, flow-stack etc.
			var viewDatabase = Database.Get<ViewDatabase>();

			var viewFactory = new ViewFactory(viewDatabase);

			var screenController = new ScreenController();
			var flowStack = new FlowStack(screenController);

			var mainMenuControllerFactory = new MainMenuControllerFactory(flowStack, viewFactory);

			//Start Flow
			var menuFlow = new MainMenuFlow(mainMenuControllerFactory);
			flowStack.Push(menuFlow);
		}
	}

}