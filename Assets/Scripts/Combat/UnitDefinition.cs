namespace AFSInterview.Combat
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Vheos.Helpers.Math;

	[CreateAssetMenu(fileName = nameof(UnitDefinition), menuName = ASSET_MENU_PATH + nameof(UnitDefinition))]
	public class UnitDefinition : ScriptableObject
	{
		public const string ASSET_MENU_PATH = nameof(Combat) + "/";

		[Header("Defense")]
		[SerializeField, Range(1, 30)] private int health = 10;
		[SerializeField, Range(0, 5)] private int armor = 0;
		[SerializeField] private UnitAttribute[] attributes = Array.Empty<UnitAttribute>();
		[Header("Offense")]
		[SerializeField, Range(1, 10)] private int cooldown = 1;
		[SerializeField, Range(1, 10)] private int damage = 1;
		[SerializeField] private DamageModifier[] damageModifiers = Array.Empty<DamageModifier>();

		public int Health => health;
		public int Armor => armor;
		public IReadOnlyList<UnitAttribute> Attributes => attributes;
		public int Cooldown => cooldown;
		public int Damage => damage;
		public IReadOnlyList<DamageModifier> DamageModifiers => damageModifiers;

		public bool HasAttribute(UnitAttribute attribute)
			=> attributes.Contains(attribute);
		public int CalculateDamageAgainst(UnitDefinition target)
		{
			int totalDamage = damage;
			foreach (var modifier in damageModifiers)
				if (target.HasAttribute(modifier.Attribute))
					totalDamage += modifier.Damage;
			totalDamage -= target.armor;

			return totalDamage.ClampMin(1);
		}
	}
}
