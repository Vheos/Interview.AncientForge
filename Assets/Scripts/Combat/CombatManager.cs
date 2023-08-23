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
		#region Serialized
		[SerializeField] private List<Team> teams;
		[SerializeField] private bool autoStartCombat = true;
		[SerializeField] private bool autoAdvanceTurns = true;
		[SerializeField, Range(0, 2f)] private float turnInterval = 1f;
		[SerializeField] private KeyCode inputNextTurn = KeyCode.G;
		#endregion

		#region Public
		public Unit ActiveUnit => units[activeUnitID];
		public bool AreEnoughCombatants => teams.Count(team => team.IsAnyAlive) >= 2;
		#endregion

		#region Private
		private bool isCombatActive = false;
		private List<Unit> units = new();
		private int activeUnitID = 0;
		private float nextTurnTime = 0f;

		private bool StartCombat()
		{
			if (!AreEnoughCombatants)
				return false;

			units = teams.SelectMany(team => team.Units).Where(unit => unit.IsAlive).ToList();
			units.Shuffle();
			isCombatActive = true;

			Debug.Log($"Combat has started!");
			Debug.Log($"Teams: {string.Join(", ", teams.Select(team => $"{team.name} (x{team.Units.Count})"))}");
			Debug.Log($"Turn order:");
			int order = 0;
			foreach (Unit unit in units)
				Debug.Log($"{++order} - {unit.FullName}");

			return true;
		}
		private bool StopCombat()
		{
			if (!isCombatActive)
				return false;

			units.Clear();
			activeUnitID = 0;
			isCombatActive = false;
			Debug.Log($"Combat has ended!");
			return true;
		}
		private void AdvanceTurn()
		{
			nextTurnTime = Time.time + turnInterval;
			activeUnitID = activeUnitID.Add(1).Mod(units.Count);

			if (ActiveUnit.TryReduceCooldown())
			{
				Debug.Log($"{ActiveUnit.FullName} is preparing ...");
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
			ActiveUnit.ResetCooldown();
			Debug.Log($"{ActiveUnit.FullName} has attacked {target.FullName} for {damage} damage!");

			CheckForDeath(target);
		}
		private void CheckForDeath(Unit target)
		{
			if (target.IsAlive)
				return;

			target.AnimateDeath(true);
			RemoveUnit(target);
			Debug.Log($"{target.FullName} has died!");

			if (!AreEnoughCombatants)
				StopCombat();
		}
		private void RemoveUnit(Unit target)
		{
			Unit cachedActiveUnit = ActiveUnit;
			units.Remove(target);
			activeUnitID = units.IndexOf(cachedActiveUnit);
		}
		#endregion

		#region Mono
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
		#endregion

	}
}
