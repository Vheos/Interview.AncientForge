namespace AFSInterview.Items.Effects
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	[CreateAssetMenu(fileName = nameof(AddItem), menuName = ASSET_MENU_PATH + nameof(AddItem))]
	public class AddItem : AItemEffectScript
	{
		public override void Invoke(InventoryController inventory, Item _, string[] values)
		{
			try
			{
				Item newItem = new(values[0], int.Parse(values[1]));
				inventory.AddItem(newItem);
			}
			catch
			{
				Debug.LogError($"Couldn't create an item from provided values: {string.Join(", ", values)}");
			}
		}
	}
}