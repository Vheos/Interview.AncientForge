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
		[field: SerializeField, FormerlySerializedAs("name")]
		public string Name { get; private set; }

		[field: SerializeField, FormerlySerializedAs("value")]
		public int Value { get; private set; }

		[field: SerializeField, FormerlySerializedAs("useEffects")]
		public ItemEffect[] UseEffects { get; private set; }

		public Item(string name, int value)
		{
			Name = name;
			Value = value;
			UseEffects = Array.Empty<ItemEffect>();
		}

		public bool IsUsable => UseEffects.Any(effect => effect.Script != null);
	}
}