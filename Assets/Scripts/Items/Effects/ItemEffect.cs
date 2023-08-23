namespace AFSInterview.Items.Effects
{
	using System;
	using UnityEngine;

	[Serializable]
	public struct ItemEffect
	{
		#region Serialized
		[SerializeField] private AItemEffectScript script;
		[SerializeField] private string data;
		#endregion

		#region Public
		public AItemEffectScript Script => script;
		public string Data => data;

		public void Invoke(InventoryController inventory, Item item)
		{
			if (script != null)
				script.Invoke(inventory, item, data.Split(';'));
		}
		#endregion
	}
}
