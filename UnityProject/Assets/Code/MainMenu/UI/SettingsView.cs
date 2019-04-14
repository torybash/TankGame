using UnityEngine;
using System.Collections;
using TankGame.Views;
using System;
using TankGame.Lifecycles;

namespace TankGame.MainMenu
{
	//public class SettingsPanel : Lifecycle<SettingsPanelLifecycleHandler>
	public class SettingsView : View
	{
		public event Action OnBack = delegate { };
		public event Action<float> OnChangedSoundVolume = delegate { };

		public void ChangedSoundVolume(float volume)
		{
			OnChangedSoundVolume(volume);
		}

		public void ClickedBack()
		{
			OnBack();
		}
	}

}
