namespace AFSInterview.Combat
{
	using DG.Tweening;
	using System;
	using UnityEngine;
	using Vheos.Helpers.Math;
	using Vheos.Helpers.UnityObjects;

	public class Unit : MonoBehaviour
	{
		#region Serialized
		[SerializeField] private UnitDefinition definition;
		[SerializeField] private UnitVisuals visualsPrefab;
		[SerializeField] private Team startingTeam;
		#endregion

		#region Events
		public event Action<int, int> OnHealthChanged = delegate { };
		public event Action<Team, Team> OnTeamChanged = delegate { };
		#endregion

		#region Public
		public int Health
		{
			get => health;
			set
			{
				if (value == health)
					return;

				int previous = health;
				health = value.Clamp(0, definition.Health);
				if (health != previous)
					OnHealthChanged(previous, health);
			}
		}
		public Team Team
		{
			get => team;
			set
			{
				if (value == team)
					return;

				Team previous = team;
				if (previous != null)
					previous.RemoveUnit(this);

				team = value;
				if (team != null)
					team.AddUnit(this);

				OnTeamChanged(previous, team);
			}
		}
		public int Cooldown { get; private set; }
		public UnitDefinition Definition => definition;
		public string FullName => $"{team.name} {definition.name} ({name})";
		public bool IsAlive => health > 0;

		public int CalculateDamageAgainst(Unit target) => definition.CalculateDamageAgainst(target.definition);
		public bool TryReduceCooldown()
		{
			if (Cooldown <= 1)
				return false;

			Cooldown--;
			return true;
		}
		public void ResetHealth() => health = definition.Health;
		public void ResetCooldown() => Cooldown = definition.Cooldown;
		public void AnimateLookAt(Unit target)
			=> transform.DOLookAt(target.transform.position, 0.25f);
		public void AnimateAttack(float strength)
			=> transform.DOPunchPosition(transform.localRotation.Far() * 0.5f.Lerp(1.5f, strength), 0.5f, 0, 0);
		public void AnimateTakeDamage(float strength)
		{
			transform.DOShakeScale(0.5f, 0.25f.Lerp(0.5f, strength));
			transform.DOShakeRotation(0.5f, 15.Lerp(30, strength));
			visuals.Material.color = Color.white;
			visuals.Material.DOColor(team.Color, 1.Lerp(2, strength));
		}
		public void AnimateDeath(bool destroyGameObject)
			=> transform.DOScale(0f, 2f).OnComplete(() => gameObject.Destroy());
		public void AnimateCooldown()
			=> transform.DOPunchScale(Vector3.down / 2f, 1f, 0, 0);
		#endregion

		#region Private
		private int health;
		private Team team;
		private UnitVisuals visuals;
		#endregion


		#region Mono
		private void Awake()
		{
			visuals = Instantiate(visualsPrefab);
			visuals.ConnectTo(this);
			Team = startingTeam;
			ResetHealth();
			ResetCooldown();
		}
		private void OnDestroy() => team.RemoveUnit(this);
		private void OnDrawGizmos()
		{
			Gizmos.color = startingTeam != null ? startingTeam.Color : Color.white;
			Gizmos.DrawWireSphere(transform.position, transform.lossyScale.CompAvg() / 2f);
			Gizmos.DrawLine(transform.position, transform.position + transform.forward);
		}
		#endregion
	}
}
