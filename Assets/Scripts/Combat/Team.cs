namespace AFSInterview.Combat
{
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	[CreateAssetMenu(fileName = nameof(Team), menuName = nameof(Combat) + "/" + nameof(Team))]
	public class Team : ScriptableObject
	{
		#region Serialized
		[SerializeField] private Color color = Color.white;
		#endregion

		#region Public
		public Color Color => color;
		public IReadOnlyCollection<Unit> Units => units;
		public bool IsAnyAlive => units.Any(unit => unit.IsAlive);

		public void AddUnit(Unit unit)
			=> units.Add(unit);
		public void RemoveUnit(Unit unit)
			=> units.Remove(unit);
		#endregion

		#region Private
		private readonly HashSet<Unit> units = new();
		#endregion


	}
}
