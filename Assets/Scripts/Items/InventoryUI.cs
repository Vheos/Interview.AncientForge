namespace AFSInterview
{
	using AFSInterview.Items;
	using System.Text;
	using TMPro;
	using UnityEngine;

	[RequireComponent(typeof(TMP_Text))]
	public class InventoryUI : MonoBehaviour
	{
		#region Serialized
		[SerializeField] private InventoryController inventory;
		[SerializeField] private string currency;
		#endregion

		#region Private
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
		#endregion

		#region Mono
		private void Awake()
		{
			if (inventory == null)
				return;

			inventory.OnMoneyChanged += UpdateText;
			inventory.OnItemsChanged += UpdateText;
			UpdateText();
		}
		#endregion
	}
}
