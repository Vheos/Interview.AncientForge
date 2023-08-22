namespace AFSInterview.Items
{
	using System;
	using UnityEngine;

	[Serializable]
	public class Item
	{
		[field: SerializeField, FormerlySerializedAs("name")]
		public string Name { get; private set; }

		[field: SerializeField, FormerlySerializedAs("value")]
		public int Value { get; private set; }

		public Item(string name, int value)
		{
			this.name = name;
			this.value = value;
		}

	}
}