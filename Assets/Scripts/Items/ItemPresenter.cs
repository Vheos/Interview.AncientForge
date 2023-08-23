namespace AFSInterview.Items
{
	using UnityEngine;

	public class ItemPresenter : MonoBehaviour, IItemHolder
	{
		#region Serialized
		[SerializeField] private Item item;
		#endregion

		#region Public
		public Item GetItem(bool disposeHolder)
		{
			if (disposeHolder)
				Destroy(gameObject);

			return item;
		}
		#endregion
	}
}