namespace AFSInterview.Items.Effects
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct ItemEffect
	{
		[SerializeField] AItemEffectScript script;
		[SerializeField] string data;

		public AItemEffectScript Script => script;
		public string Data => data;

		public void Invoke(InventoryController inventory, Item item)
		{
			if (script != null)
				script.Invoke(inventory, item, data.Split(';'));
		}
	}
}
