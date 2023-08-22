namespace AFSInterview.Items.Effects
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	[Serializable]
	public struct ItemEffect
	{
		public AItemEffectScript Script;
		public string Data;

		public void Invoke(InventoryController inventory, Item item)
		{
			if (Script != null)
				Script.Invoke(inventory, item, Data.Split(';'));
		}
	}
}
