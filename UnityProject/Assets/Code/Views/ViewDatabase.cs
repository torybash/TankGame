using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TankGame.Databases;

namespace TankGame.Views
{
	[CreateAssetMenu]
	public class ViewDatabase : Database
	{
		[SerializeField] private List<View> views;

		public IView GetViewPrefab<T>() where T : IView
		{
			return views.FirstOrDefault(x => x is T);
		}
	}
}