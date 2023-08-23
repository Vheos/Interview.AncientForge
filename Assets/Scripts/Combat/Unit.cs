namespace AFSInterview.Combat
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Vheos.Helpers.Math;

	public class Unit : MonoBehaviour
	{
		[SerializeField] private UnitDefinition definition;
		[SerializeField] private UnitVisuals visualsPrefab;
		[SerializeField] private Team startingTeam;

		private int health;
		private Team team;
		public event Action<Team, Team> OnTeamChanged = delegate { };

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
				team = value;
				OnTeamChanged(previous, team);
			}
		}
		public UnitDefinition Definition => definition;
		public bool IsAlive => health > 0;
		public void ResetHealth() => health = definition.Health;
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

		private void Awake()
		{
			Instantiate(visualsPrefab).ConnectTo(this);
			Team = startingTeam;
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = startingTeam != null ? startingTeam.Color : Color.white;
			Gizmos.DrawWireSphere(transform.position, transform.lossyScale.CompAvg() / 2f);
			Gizmos.DrawLine(transform.position, transform.position + transform.forward);
		}
	}
}
