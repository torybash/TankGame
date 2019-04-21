using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TankGame.Views
{
	public class ViewController
	{
		private readonly ViewDatabase viewDatabase;

		private readonly List<View> viewStack = new List<View>();

		private View CurrentView {
			get {
				if (viewStack.Count == 0) return null;
				return viewStack[viewStack.Count - 1];
			}
		}

		public ViewController(ViewDatabase viewDatabase)
		{
			this.viewDatabase = viewDatabase;
		}

		public T ShowView<T>() where T : View
		{
			var prefab = viewDatabase.GetViewPrefab<T>();
			if (prefab != null)
			{
				var instance = Object.Instantiate((T)prefab);
				instance.SetCameraDepth(viewStack.Count);
				instance.OnClosed += ClosedView;
				viewStack.Add(instance);
				return instance;
			}
			return default;
		}

		public T ShowViewComponent<T>() where T : MonoBehaviour
		{
			var prefab = viewDatabase.GetViewComponent<T>();
			if (prefab != null)
			{
				var parent = CurrentView.transform;
				var instance = Object.Instantiate(prefab, parent);
				
				return instance;
			}
			return default;
		}

		private void ClosedView(View view)
		{
			viewStack.Remove(view);
		}

		public Camera GetViewCamera()
		{
			return CurrentView.Camera;
		}
	}
}
