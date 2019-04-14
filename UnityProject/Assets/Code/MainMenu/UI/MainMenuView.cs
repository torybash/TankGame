using System;
using TankGame.Views;

namespace TankGame.MainMenu
{
	public class MainMenuView : View
	{
		public event Action OnStart = delegate { };
		public event Action OnSettings = delegate { };
		public event Action OnQuit = delegate { };

		public void ClickedStart()
		{
			OnStart();
		}

		public void ClickedSettings()
		{
			OnSettings();
		}

		public void ClickedQuit()
		{
			OnQuit();
		}

	}
}
