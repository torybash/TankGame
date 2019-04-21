using System.Collections.Generic;
using System.Linq;
using TankGame.Databases;
using UnityEngine;

namespace TankGame.Views
{
	[CreateAssetMenu]
	public class ViewDatabase : Database
	{
		[SerializeField] private List<View> views;
		[SerializeField] private List<MonoBehaviour> viewComponents;

		public IView GetViewPrefab<T>() where T : IView
		{
			return views.FirstOrDefault(x => x is T);
		}

		public T GetViewComponent<T>() where T : MonoBehaviour
		{
			return (T)viewComponents.FirstOrDefault(x => x is T);
		}
	}
}