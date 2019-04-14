using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.Flow
{
	public interface IFlow
	{
		IEnumerator Flow();
		void Entered();
		void Ended();
	}

}