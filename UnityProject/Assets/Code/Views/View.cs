using System;
using UnityEngine;

namespace TankGame.Views
{
	public abstract class View : MonoBehaviour, IView
	{
		public event Action<View> OnClosed = delegate { };

		[SerializeField] private Camera cam;

		public Camera Camera {
			get { return cam; }
		}

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
