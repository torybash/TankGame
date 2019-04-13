using UnityEngine;
using System.Collections;

namespace TankGame.Views
{
	public abstract class View : MonoBehaviour, IView
	{
		public virtual bool IsVisible()
		{
			return gameObject.activeSelf;
		}

	}
}
