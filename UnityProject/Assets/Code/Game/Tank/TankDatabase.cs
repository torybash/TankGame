using UnityEngine;
using System.Collections;
using TankGame.Databases;
using System.Collections.Generic;
using System.Linq;

namespace TankGame.Game
{
	[CreateAssetMenu]
	public class TankDatabase : Database
	{
		[SerializeField] List<TankState> tankStates;

		public TankState GetTankState(string id)
		{
			var state = tankStates.FirstOrDefault(x => x.id == id);
			return (TankState)state.Clone();
		}
	}
}