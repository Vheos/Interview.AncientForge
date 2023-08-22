namespace AFSInterview.Items
{
	using System.Collections.Generic;
	using UnityEngine;

	public class InventoryController : MonoBehaviour
	{
		[field: SerializeField] public List<Item> Items { get; private set; }
		[field: SerializeField] public int Money { get; private set; }

		public event Action OnItemsChanged = delegate { };

		public void SellAllItemsUpToValue(int maxValue)
		{
			for (var i = 0; i < items.Count; i++)
			{
				var itemValue = items[i].Value;
				if (itemValue > maxValue)
					continue;
				
				money += itemValue;
				items.RemoveAt(i);
			}
		}

		public void AddItem(Item item)
		{
			Items.Add(item);
			OnItemsChanged();
		}
	}
}