namespace AFSInterview.Combat
{
	using DG.Tweening;
	using UnityEngine;
	using Vheos.Helpers.UnityObjects;

	[RequireComponent(typeof(Renderer))]
	public class UnitVisuals : MonoBehaviour
	{
		#region Public
		public Material Material { get; private set; }
		public void ConnectTo(Unit unit)
		{
			this.BecomeChildOf(unit);
			Material = GetComponent<Renderer>().material;
			unit.OnTeamChanged += AnimateColor;
		}
		#endregion

		#region Private
		private void AnimateColor(Team previous, Team current)
		{
			Color color = current != null ? current.Color : Color.white;
			Material.DOColor(color, 1f);
		}
		#endregion
	}
}