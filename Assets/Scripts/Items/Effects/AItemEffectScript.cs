namespace AFSInterview.Items.Effects
{
	using UnityEngine;

	public abstract class AItemEffectScript : ScriptableObject
	{
		public const string ASSET_MENU_PATH = nameof(Items) + "/" + nameof(Effects) + "/";

		public abstract void Invoke(InventoryController inventory, Item item, string[] values);
	}
}
