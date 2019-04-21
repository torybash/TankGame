using UnityEngine;
using System.Collections;
using TankGame.Views;
using System;

namespace TankGame.Game
{
	public class WorldMapView : View
	{
		public event Action<string> OnDestinationSelected = delegate { };

		public void ClickedStart()
		{
			OnDestinationSelected("Test");
		}
	}
}