namespace AFSInterview.Assets.Scripts.Combat
{
	using AFSInterview.Combat;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Vheos.Helpers.Collections;
	using Vheos.Helpers.Math;
	using Vheos.Helpers.RNG;

	public class CombatManager : MonoBehaviour
	{
		[SerializeField] private List<Team> teams;
		[SerializeField] private bool autoStartCombat = true;
		[SerializeField] private bool autoAdvanceTurns = true;
		[SerializeField, Range(0, 2f)] private float turnInterval = 1f;
		[SerializeField] private KeyCode inputNextTurn = KeyCode.G;

		private List<Unit> units = new();
		private int activeUnitID = 0;
		private Unit ActiveUnit => units[activeUnitID];
		private float nextTurnTime = 0f;
		private bool isCombatActive = false;
		public bool AreEnoughCombatants => teams.Count(team => team.IsAnyAlive) >= 2;

		private void Start()
		{
			if (autoStartCombat)
				StartCombat();
		}
		private void Update()
		{
			if (!isCombatActive)
				return;

			if (Input.GetKeyDown(inputNextTurn)
			|| autoAdvanceTurns && Time.time > nextTurnTime)
				AdvanceTurn();
		}

		private bool StartCombat()
		{
			if (!AreEnoughCombatants)
				return false;

			units = teams.SelectMany(team => team.Units).Where(unit => unit.IsAlive).ToList();
			units.Shuffle();
			isCombatActive = true;
			return true;
		}
		private bool StopCombat()
		{
			if (!isCombatActive)
				return false;

			units.Clear();
			activeUnitID = 0;
			isCombatActive = false;
			return true;
		}
		private void AdvanceTurn()
		{
			nextTurnTime = Time.time + turnInterval;
			activeUnitID = activeUnitID.Add(1).Mod(units.Count);
			if (ActiveUnit.TryReduceCooldown())
			{
				ActiveUnit.AnimateCooldown();
				return;
			}

			Unit target = units.Where(unit => unit.Team != ActiveUnit.Team).ToArray().Random();
			int damage = ActiveUnit.CalculateDamageAgainst(target);

			float animationStrength = damage / 10f;
			ActiveUnit.AnimateLookAt(target);
			ActiveUnit.AnimateAttack(animationStrength);
			target.AnimateLookAt(ActiveUnit);
			target.AnimateTakeDamage(animationStrength);

			target.Health -= damage;
			CheckForDeath(target);
			ActiveUnit.ResetCooldown();
		}

		private void CheckForDeath(Unit target)
		{
			if (target.IsAlive)
				return;

			target.AnimateDeath(true);
			RemoveUnit(target);
			if (!AreEnoughCombatants)
				StopCombat();
		}

		private void RemoveUnit(Unit target)
		{
			Unit cachedActiveUnit = ActiveUnit;
			units.Remove(target);
			activeUnitID = units.IndexOf(cachedActiveUnit);
		}
	}
}
