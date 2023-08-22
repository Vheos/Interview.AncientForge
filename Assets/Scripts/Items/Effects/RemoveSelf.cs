namespace AFSInterview
{
	using AFSInterview.Items;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Serialization;

	[CreateAssetMenu(fileName = nameof(RemoveSelf), menuName = ASSET_MENU_PATH + nameof(RemoveSelf))]
	public class RemoveSelf : ItemEffectScript
	{
		public override void Invoke(InventoryController inventory, Item item, string[] _)
			=> inventory.RemoveItem(item);
	}
}
