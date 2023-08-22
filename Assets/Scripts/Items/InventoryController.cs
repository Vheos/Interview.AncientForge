namespace AFSInterview.Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using UnityEngine.Serialization;
	using Vheos.Helpers;
	using Vheos.Helpers.Collections;
	using Vheos.Helpers.Math;

	public class InventoryController : MonoBehaviour
	{
		[SerializeField] private int money;
		[SerializeField] private List<Item> items;

		public int Money
		{
			get => money;
			set
			{
				int previous = money;
				money = value.ClampMin(0);
				if (money != previous)
					OnMoneyChanged();
			}
		}
		public IReadOnlyList<Item> Items => items;

		public event Action OnMoneyChanged = delegate { };
		public event Action OnItemsChanged = delegate { };

		public void SellAllItemsUpToValue(int maxValueIncl)
		{
			Item[] filteredItems = items.Where(item => item.Value <= maxValueIncl).ToArray();
			if (filteredItems.Length == 0)
				return;

			int totalValue = filteredItems.Sum(item => item.Value);
			Debug.Log($"Sold {filteredItems.Length} items for a total value of {totalValue}");
			Money += totalValue;
			RemoveItems(filteredItems);
		}

		public void AddItem(Item item)
		{
			if (items.TryAddUnique(item))
				OnItemsChanged();
		}

		public void AddItems(IEnumerable<Item> items)
		{
			if (this.items.TryAddUnique(items))
				OnItemsChanged();
		}

		public void RemoveItem(Item item)
		{
			if (items.TryRemove(item))
				OnItemsChanged();
		}

		public void RemoveItems(IEnumerable<Item> items)
		{
			if (this.items.TryRemove(items))
				OnItemsChanged();
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
			=> UseItem(items.FirstOrDefault(item => item.IsUsable));
	}
}