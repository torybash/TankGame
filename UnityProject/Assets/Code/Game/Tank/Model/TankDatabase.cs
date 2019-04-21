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
		[SerializeField] List<TankAbility> tankAbilities;

		public TankState GetTankState(string id)
		{
			var state = tankStates.FirstOrDefault(x => x.id == id);
			return (TankState)state.Clone();
		}

		public TankAbility GetTankAbility(string id)
		{
			var state = tankAbilities.FirstOrDefault(x => x.id == id);
			return state;
		}
	}
}