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

		public event Action<Team, Team> OnTeamChanged = delegate { };

		public UnitDefinition Definition => definition;

		private Team team;
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
