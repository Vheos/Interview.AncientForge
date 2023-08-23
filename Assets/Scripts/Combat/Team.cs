namespace AFSInterview.Combat
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	[CreateAssetMenu(fileName = nameof(Team), menuName = ASSET_MENU_PATH + nameof(Team))]
	public class Team : ScriptableObject
	{
		public const string ASSET_MENU_PATH = nameof(Combat) + "/";

		[SerializeField] private Color color = Color.white;
		private readonly HashSet<Unit> units = new();

		public Color Color => color;
		public IReadOnlyCollection<Unit> Units => units;
	}
}
