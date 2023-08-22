namespace AFSInterview
{
	using AFSInterview.Items;
	using System.Collections;
	using System.Collections.Generic;
	using System.Text;
	using TMPro;
	using UnityEngine;
	using UnityEngine.Serialization;

	[RequireComponent(typeof(TMP_Text))]
	public class InventoryUI : MonoBehaviour
	{
		[SerializeField] private InventoryController inventory;
		[SerializeField] private string currency;

		private void Awake()
		{
			if (inventory != null)
			{
				inventory.OnMoneyChanged += UpdateText;
				inventory.OnItemsChanged += UpdateText;
				UpdateText();
			}
		}

		private void UpdateText()
		{
			StringBuilder sb = new();
			sb.AppendLine($"{inventory.Money}{currency}");
			sb.AppendLine();
			sb.AppendLine($"{nameof(inventory.Items)}");
			foreach (var item in inventory.Items)
				sb.AppendLine($"- {item.Name} ({item.Value}{currency})");

			GetComponent<TMP_Text>().text = sb.ToString();
		}
	}
}
