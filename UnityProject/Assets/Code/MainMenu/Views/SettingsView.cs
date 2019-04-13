using UnityEngine;
using System.Collections;
using TankGame.Views;
using System;

namespace TankGame.MainMenu
{
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
