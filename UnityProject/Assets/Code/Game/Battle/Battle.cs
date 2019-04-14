using System;
using System.Collections;
using TankGame.Views;

namespace TankGame.Game
{
	public class Battle
	{
		private readonly ViewController viewController;

		private GameState gameState;

		public Battle(ViewController viewController, GameState gameState)
		{
			this.viewController = viewController;
			this.gameState = gameState;
		}

		public IEnumerator Run()
		{
			var battleView = viewController.ShowView<BattleView>();

			while (battleView.IsVisible())
			{
				yield return null;
			}
		}
	}
}