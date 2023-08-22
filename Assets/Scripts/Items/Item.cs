namespace AFSInterview.Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using UnityEngine.Serialization;

	[Serializable]
	public class Item
	{
		[SerializeField] private string name;
		[SerializeField] private int value;
		[SerializeField] private ItemEffect[] useEffects;

		public string Name => name;
		public int Value => value;
		public IReadOnlyList<ItemEffect> UseEffects => useEffects;

		public Item(string name, int value)
		{
			this.name = name;
			this.value = value;
			useEffects = Array.Empty<ItemEffect>();
		}

		public bool IsUsable => useEffects.Any(effect => effect.Script != null);
	}
}