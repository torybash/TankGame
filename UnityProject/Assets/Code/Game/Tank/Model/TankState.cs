﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TankGame.Game
{
	[Serializable]
	public class TankState : ICloneable
	{
		public string id;
		public int hullHp;
		public int maxHp;
		public List<TankSectionState> tankSectionState;

		public object Clone()
		{
			return new TankState
			{
				id = id,
				maxHp = maxHp,
				hullHp = hullHp,
				tankSectionState = tankSectionState.Clone()
			};
		}
	}

}

