namespace AFSInterview.Items.Effects
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	public abstract class AItemEffectScript : ScriptableObject
	{
		public const string ASSET_MENU_PATH = "Items/Effects/";

		public abstract void Invoke(InventoryController inventory, Item item, string[] values);
	}
}
