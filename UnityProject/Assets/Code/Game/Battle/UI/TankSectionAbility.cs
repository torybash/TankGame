using System;
using UnityEngine;

namespace TankGame.Game
{
	public class TankSectionAbility : MonoBehaviour
	{
		public TankAbility TankAbility { get; internal set; }

		public event Action OnPressed = delegate { };
		public event Action OnHover = delegate { };

		public void OnPressedTriggered()
		{
			Debug.Log("OnPressed");
			OnPressed();
		}

		public void OnHoverTriggered()
		{
			Debug.Log("OnHover");
			OnHover();
		}


	}
}
