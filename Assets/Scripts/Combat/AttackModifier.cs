namespace AFSInterview.Combat
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct DamageModifier
	{
		#region Serialized
		[SerializeField] public UnitAttribute attribute;
		[SerializeField, Range(-5, 5)] public int damage;
		#endregion

		#region Public
		public UnitAttribute Attribute => attribute;
		public int Damage => damage;
		#endregion
	}
}
