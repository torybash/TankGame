using System.Collections;
using System.Collections.Generic;
using TankGame.Flow;
using TankGame.Views;
using UnityEngine;

namespace TankGame.MainMenu
{
	public class MainMenuControllerFactory
	{
		private readonly FlowStack flowStack;
		private readonly ViewFactory viewFactory;

		public MainMenuControllerFactory(FlowStack flowStack, ViewFactory viewFactory)
		{
			this.flowStack = flowStack;
			this.viewFactory = viewFactory;
		}

		public MainMenu GetMainMenu()
		{
			var mainMenu = new MainMenu(flowStack, viewFactory);
			return mainMenu;
		}
	}

}