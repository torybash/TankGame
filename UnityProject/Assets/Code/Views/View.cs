using UnityEngine;
using System.Collections;
using System;

namespace TankGame.Views
{
	public abstract class View : MonoBehaviour, IView
	{
		public event Action<View> OnClosed = delegate { };

		[SerializeField] private Camera cam;


		public void SetCameraDepth(int depth)
		{
			cam.depth = depth;
		}

		public virtual bool IsVisible()
		{
			return gameObject.activeSelf;
		}

		public void Close()
		{
			gameObject.SetActive(false);
			OnClosed(this);
		}

	}
}
