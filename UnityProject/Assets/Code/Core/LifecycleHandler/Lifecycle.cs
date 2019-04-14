using UnityEngine;
using System.Collections;

namespace TankGame.Lifecycles
{
	public class Lifecycle<T> : MonoBehaviour, ILifecycle
	{


		private void Awake()
		{
			LifecycleHandlerHelper.Awake(this);
		}
	}

}