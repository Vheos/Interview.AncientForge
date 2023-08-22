namespace AFSInterview
{
	using AFSInterview.Items;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Serialization;

	[CreateAssetMenu(fileName = nameof(ChangeMoney), menuName = ASSET_MENU_PATH + nameof(ChangeMoney))]
	public class ChangeMoney : ItemEffectScript
	{
		public override void Invoke(InventoryController inventory, Item item, string[] values)
			=> inventory.ChangeMoney(int.Parse(values[0]));
	}
}
