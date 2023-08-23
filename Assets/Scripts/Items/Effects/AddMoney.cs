namespace AFSInterview.Items.Effects
{
	using UnityEngine;

	[CreateAssetMenu(fileName = nameof(AddMoney), menuName = ASSET_MENU_PATH + nameof(AddMoney))]
	public class AddMoney : AItemEffectScript
	{
		public override void Invoke(InventoryController inventory, Item item, string[] values)
			=> inventory.Money += int.Parse(values[0]);
	}
}
