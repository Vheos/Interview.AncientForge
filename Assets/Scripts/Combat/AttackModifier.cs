namespace AFSInterview.Combat
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	[Serializable]
	public struct DamageModifier
	{
		public UnitAttribute Attribute;
		[Range(-5, 5)] public int Damage;
	}
}
