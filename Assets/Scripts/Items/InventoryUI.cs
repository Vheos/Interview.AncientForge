namespace AFSInterview
{
	using AFSInterview.Items;
	using System.Collections;
	using System.Collections.Generic;
	using System.Text;
	using TMPro;
	using UnityEngine;

	[RequireComponent(typeof(TMP_Text))]
	public class InventoryUI : MonoBehaviour
	{
		[field: SerializeField] public InventoryController Controller { get; private set; }
		[field: SerializeField] public string CurrencyPostfix { get; private set; }

		private void Awake()
		{
			if (Controller != null)
			{
				Controller.OnInventoryChanged += UpdateText;
				UpdateText();
			}
		}

		private void UpdateText()
		{
			StringBuilder sb = new();
			sb.AppendLine($"{Controller.Money}{CurrencyPostfix}");
			sb.AppendLine();
			sb.AppendLine($"{nameof(Controller.Items)}");
			foreach (var item in Controller.Items)
				sb.AppendLine($"- {item.Name} ({item.Value}{CurrencyPostfix})");

			GetComponent<TMP_Text>().text = sb.ToString();
		}
	}
}
