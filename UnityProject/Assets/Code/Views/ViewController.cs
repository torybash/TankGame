using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Views
{
	public class ViewController
	{
		private readonly ViewDatabase viewDatabase;

		private readonly List<View> viewStack = new List<View>();


		public ViewController(ViewDatabase viewDatabase)
		{
			this.viewDatabase = viewDatabase;
		}

		internal T ShowView<T>() where T : View
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

		private void ClosedView(View view)
		{
			viewStack.Remove(view);
		}
	}
}
