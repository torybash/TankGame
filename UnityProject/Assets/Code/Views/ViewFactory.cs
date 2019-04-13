using UnityEngine;
using System.Collections;

namespace TankGame.Views
{
	public class ViewFactory
	{
		private readonly ViewDatabase viewDatabase;

		public ViewFactory(ViewDatabase viewDatabase)
		{
			this.viewDatabase = viewDatabase;
		}

		public T GetView<T>() where T : View
		{
			var view = viewDatabase.GetViewPrefab<T>();
			if (view != null)
			{
				return Object.Instantiate((T)view);
			}
			return default;
		}

	}
}