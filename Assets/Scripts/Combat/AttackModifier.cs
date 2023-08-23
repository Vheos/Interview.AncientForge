namespace AFSInterview.Combat
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct DamageModifier
	{
		[SerializeField] public UnitAttribute attribute;
		[SerializeField, Range(-5, 5)] public int damage;

		public UnitAttribute Attribute => attribute;
		public int Damage => damage;
	}
}
