namespace AFSInterview
{
	using AFSInterview.Items;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[Serializable]
	public struct ItemEffect
	{
		public ItemEffectScript Script;
		public string Data;

		public void Invoke(InventoryController inventory, Item item)
		{
			if (Script != null)
				Script.Invoke(inventory, item, Data.Split(';'));
		}
	}
}
