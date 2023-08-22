namespace AFSInterview.Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	public class InventoryController : MonoBehaviour
	{
		[field: SerializeField, FormerlySerializedAs("items")]
		public List<Item> Items { get; private set; }

		[field: SerializeField, FormerlySerializedAs("money")]
		public int Money { get; private set; }

		public event Action OnInventoryChanged = delegate { };

		public void SellAllItemsUpToValue(int maxValueIncl)
		{
			Item[] filteredItems = Items.Where(item => item.Value <= maxValueIncl).ToArray();
			if (filteredItems.Length == 0)
				return;

			foreach (var item in filteredItems)
			{
				Money += item.Value;
				Items.Remove(item);
			}
			OnInventoryChanged();
		}

		public void AddItem(Item item)
		{
			Items.Add(item);
			OnInventoryChanged();
		}
	}
}