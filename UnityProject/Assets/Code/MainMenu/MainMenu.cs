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
		private readonly ViewFactory viewFactory;

		private FlowStack flowStack;

		private MainMenuView mainMenuView;

		public MainMenu(FlowStack flowStack, ViewFactory viewFactory)
		{
			this.flowStack = flowStack;
			this.viewFactory = viewFactory;
		}

		public IEnumerator Run()
		{
			mainMenuView = viewFactory.GetView<MainMenuView>();
			mainMenuView.OnStart += OnStart;
			mainMenuView.OnSettings += OnSettings;
			mainMenuView.OnQuit += OnQuit;
			
			while (mainMenuView.IsVisible())
			{
				yield return null;
			}
		}

		private void OnStart()
		{
			var gameFlow = new GameFlow();
			flowStack.Push(gameFlow);
		}

		private void OnSettings()
		{

		}

		private void OnQuit()
		{
			Application.Quit();
		}
	}

}