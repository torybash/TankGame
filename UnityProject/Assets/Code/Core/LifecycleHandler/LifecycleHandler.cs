using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Lifecycles
{
	public class LifecycleHandler<T> where T : ILifecycle
	{
		public virtual void InstanceAwake(T instance) { }

		public LifecycleHandler()
		{
			LifecycleHandlerHelper.AddTrigger<T>(InstanceAwake);
		}
	}

	public class LifecycleHandlerHelper
	{
		private static Dictionary<Type, Action<ILifecycle>> OnAwakeCalls = new Dictionary<Type, Action<ILifecycle>>();

		internal static void AddTrigger<T>(Action<T> action) where T : ILifecycle
		{
			if (!OnAwakeCalls.ContainsKey(typeof(T)))
			{
				OnAwakeCalls.Add(typeof(T), (_) => { });
			}
			OnAwakeCalls[typeof(T)] += (x) => action((T)x);
		}

		public static void Awake(ILifecycle lifecycle)
		{
			var type = lifecycle.GetType();
			if (!OnAwakeCalls.ContainsKey(lifecycle.GetType()))
			{
				OnAwakeCalls.Add(type, (_) => { });
			}
			OnAwakeCalls[type].Invoke(lifecycle);
		}
	}
}