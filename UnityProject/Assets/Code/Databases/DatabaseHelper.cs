using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TankGame.Databases
{
	public class DatabaseHelper : MonoBehaviour
	{
		private static DatabaseHelper instance;

		[SerializeField] private List<Database> databases;

		public List<Database> Databases {
			get { return databases; }
		}

		[InitializeOnLoadMethod]
		private static void Initialize()
		{
			if (instance == null)
			{
				instance = FindObjectOfType<DatabaseHelper>();

				if (instance == null)
				{
					instance = new GameObject("DatabaseHelper").AddComponent<DatabaseHelper>();
					instance.gameObject.hideFlags = HideFlags.NotEditable;
				}
			}

			instance.UpdateDatabaseReferences();
		}

		private void UpdateDatabaseReferences()
		{
			databases = Resources.FindObjectsOfTypeAll<Database>().ToList();
		}

		public T Get<T>() where T : Database
		{
			var instance = databases.FirstOrDefault(x => x.GetType() == typeof(T));
			if (instance == null)
			{
				var databases = FindObjectOfType<DatabaseHelper>().Databases;
				instance = databases.FirstOrDefault(x => x.GetType() == typeof(T));

				if (instance != null)
				{
					databases.Add(instance);
				}
			}
			return (T)instance;
		}
	}
}