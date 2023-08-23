namespace AFSInterview.Combat
{
	using DG.Tweening;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Vheos.Helpers.UnityObjects;

	[RequireComponent(typeof(Renderer))]
	public class UnitVisuals : MonoBehaviour
	{
		internal void ConnectTo(Unit unit)
		{
			this.BecomeChildOf(unit);
			unit.OnTeamChanged += UpdateColor;
		}

		private void UpdateColor(Team previous, Team current)
		{
			Color color = current != null ? current.Color : Color.white;
			GetComponent<Renderer>().material.DOColor(color, UnityEngine.Random.Range(0.5f, 2f));
		}
	}
}