namespace AFSInterview
{
	using AFSInterview.Items;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Serialization;

	[CreateAssetMenu(fileName = nameof(AddItem), menuName = ASSET_MENU_PATH + nameof(AddItem))]
	public class AddItem : ItemEffectScript
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