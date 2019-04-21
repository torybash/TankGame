using System;
using System.Collections;
using TankGame.Flow;
using TankGame.Game;
using TankGame.Views;
using UnityEngine;

namespace TankGame.MainMenu
{
	public class MainMenu
	{
		private readonly ViewController viewController;
		private readonly GameControllerFactory gameControllerFactory;

		private FlowStack flowStack;

		public MainMenu(FlowStack flowStack, ViewController viewController, GameControllerFactory gameControllerFactory)
		{
			this.flowStack = flowStack;
			this.viewController = viewController;
			this.gameControllerFactory = gameControllerFactory;
		}

		public void Run()
		{
			var mainMenuView = viewController.ShowView<MainMenuView>();
			mainMenuView.OnStart += OnStart;
			mainMenuView.OnSettings += OnSettings;
			mainMenuView.OnQuit += OnQuit;
		}

		private void OnStart()
		{
			var gameFlow = new GameFlow(gameControllerFactory);
			flowStack.Push(gameFlow);
		}

		private void OnSettings()
		{
			var settingsView = viewController.ShowView<SettingsView>();
			settingsView.OnBack += settingsView.Close;
		}

		private void OnQuit()
		{
			Application.Quit();
		}
	}

}