namespace AFSInterview.Items
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	public class ItemPresenter : MonoBehaviour, IItemHolder
	{
		[SerializeField] private Item item;
        
		public Item GetItem(bool disposeHolder)
		{
			if (disposeHolder)
				Destroy(gameObject);
			
			return item;
		}
	}
}