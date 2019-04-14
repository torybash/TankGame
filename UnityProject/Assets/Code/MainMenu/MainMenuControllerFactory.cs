using System.Collections;
using System.Collections.Generic;
using TankGame.Flow;
using TankGame.Game;
using TankGame.Views;
using UnityEngine;

namespace TankGame.MainMenu
{
	public class MainMenuControllerFactory
	{
		private readonly FlowStack flowStack;
		private readonly ViewController viewController;
		private readonly GameControllerFactory gameControllerFactory;

		public MainMenuControllerFactory(FlowStack flowStack, ViewController viewController, GameControllerFactory gameControllerFactory)
		{
			this.flowStack = flowStack;
			this.viewController = viewController;
			this.gameControllerFactory = gameControllerFactory;
		}

		public MainMenu GetMainMenu()
		{
			var mainMenu = new MainMenu(flowStack, viewController, gameControllerFactory);
			return mainMenu;
		}
	}

}