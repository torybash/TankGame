using UnityEngine;
using System.Collections;

namespace TankGame.Lifecycles
{
	public abstract class Lifecycle<T> : MonoBehaviour, ILifecycle
	{


		protected virtual void Awake()
		{
			LifecycleHandlerHelper.Awake(this);
		}
	}

}