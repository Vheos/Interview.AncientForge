namespace AFSInterview.Items.Effects
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	[CreateAssetMenu(fileName = nameof(RemoveSelf), menuName = ASSET_MENU_PATH + nameof(RemoveSelf))]
	public class RemoveSelf : AItemEffectScript
	{
		public override void Invoke(InventoryController inventory, Item item, string[] _)
			=> inventory.RemoveItem(item);
	}
}
