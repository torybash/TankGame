using UnityEditor;
using UnityEngine;

namespace TankGame.Databases
{
	public class DatabaseHelper : MonoBehaviour
	{
		private static DatabaseHelper instance;

		[SerializeField] private Database[] databases;

		public Database[] Databases {
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
			databases = Resources.FindObjectsOfTypeAll<Database>();
		}
	}
}