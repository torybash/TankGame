using UnityEngine;
using System.Collections;
using TankGame.Views;
using System;

namespace TankGame.Game
{
	public class BattleView : View
	{
		public event Action OnEndTurn = delegate { };

		public void ClickedEndTurn()
		{
			OnEndTurn();
		}
	}
}