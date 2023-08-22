namespace AFSInterview
{
	using AFSInterview.Items;
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public abstract class ItemEffectScript : ScriptableObject
	{
		public const string ASSET_MENU_PATH = "ItemEffects/";

		public abstract void Invoke(InventoryController inventory, Item item, string[] values);
	}
}
