using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TankGame.Databases
{

	public abstract class Database : ScriptableObject
	{
		private static readonly List<Database> instances = new List<Database>();

		public static T Get<T>() where T : Database
		{
			var instance = instances.FirstOrDefault(x => x.GetType() == typeof(T));
			if (instance == null)
			{
				var databases = FindObjectOfType<DatabaseHelper>().Databases;
				instance = databases.FirstOrDefault(x => x.GetType() == typeof(T));

				if (instance != null)
				{
					instances.Add(instance);
				}
			}
			try
			{
				instance = (T)instance;
			} catch (Exception e)
			{
				Debug.LogError(e + "\ntypeof(T):" + typeof(T).ToString() + " instance: " + instance + " , instance.GetType: " + (instance != null ? instance.GetType().ToString() : "NULL"));
			}
			return (T)instance;
		}

	}
}

