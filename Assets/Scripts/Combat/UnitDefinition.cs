namespace AFSInterview.Combat
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	[CreateAssetMenu(fileName = nameof(UnitDefinition), menuName = ASSET_MENU_PATH + nameof(UnitDefinition))]
	public class UnitDefinition : ScriptableObject
	{
		public const string ASSET_MENU_PATH = nameof(Combat) + "/";

		[Header("Defense")]
		[SerializeField, Range(1, 20)] private int health;
		[SerializeField, Range(0, 10)] private int armor;
		[SerializeField] private UnitAttribute[] attributes;
		[Header("Offense")]
		[SerializeField, Range(1, 10)] private int interval;
		[SerializeField, Range(1, 10)] private int damage;
		[SerializeField] private DamageModifier[] damageModifiers;

		public int Health => health;
		public int Armor => armor;
		public IReadOnlyList<UnitAttribute> Attributes => attributes;
		public int Interval => interval;
		public int Damage => damage;
		public IReadOnlyList<DamageModifier> DamageModifiers => damageModifiers;
	}
}
