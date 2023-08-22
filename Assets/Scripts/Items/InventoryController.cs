namespace AFSInterview.Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using UnityEngine.Serialization;

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

		public bool UseItem(Item item)
		{
			if (item == null || !item.IsUsable)
				return false;

			foreach (var effect in item.UseEffects)
				effect.Invoke(this, item);

			return true;
		}

		public bool UseFirstUsableItem()
		=> UseItem(Items.FirstOrDefault(item => item.IsUsable));

		public void AddItem(Item item)
		{
			Items.Add(item);
			OnInventoryChanged();
		}

		public void RemoveItem(Item item)
		{
			if (Items.Remove(item))
				OnInventoryChanged();
		}

		public void ChangeMoney(int change)
		{
			int previousValue = Money;

			Money += change;
			if (Money < 0)
				Money = 0;

			if (Money != previousValue)
				OnInventoryChanged();
		}
	}
}