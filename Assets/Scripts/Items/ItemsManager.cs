namespace AFSInterview.Items
{
	using UnityEngine;
	using Random = UnityEngine.Random;

	public class ItemsManager : MonoBehaviour
	{
		#region Serialized
		[SerializeField] private InventoryController inventoryController = null;
		[SerializeField] private int itemSellMaxValue = 150;
		[SerializeField] private Transform itemSpawnParent = null;
		[SerializeField] private GameObject itemPrefab = null;
		[SerializeField] private BoxCollider itemSpawnArea = null;
		[SerializeField, Range(1f, 10f)] private float itemSpawnInterval = 2.5f;
		[SerializeField] private KeyCode inputSell = KeyCode.Space;
		[SerializeField] private KeyCode inputUse = KeyCode.Tab;
		#endregion

		#region Private
		private float nextItemSpawnTime;

		private void SpawnNewItem()
		{
			nextItemSpawnTime = Time.time + itemSpawnInterval;

			var spawnAreaBounds = itemSpawnArea.bounds;
			var position = new Vector3(
				Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x),
				0f,
				Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z)
			);

			Instantiate(itemPrefab, position, Quaternion.identity, itemSpawnParent);
		}
		private bool TryPickUpItem()
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var layerMask = LayerMask.GetMask("Item");
			if (!Physics.Raycast(ray, out var hit, 100f, layerMask) || !hit.collider.TryGetComponent<IItemHolder>(out var itemHolder))
				return false;

			var item = itemHolder.GetItem(true);
			inventoryController.AddItem(item);
			Debug.Log("Picked up " + item.Name + " with value of " + item.Value + " and now have " + inventoryController.Items.Count + " items");
			return true;
		}
		#endregion

		#region Mono
		private void Update()
		{
			if (Time.time >= nextItemSpawnTime)
				SpawnNewItem();

			if (Input.GetMouseButtonDown(0))
				TryPickUpItem();

			if (Input.GetKeyDown(inputSell))
				inventoryController.SellAllItemsUpToValue(itemSellMaxValue);

			if (Input.GetKeyDown(inputUse))
				inventoryController.UseFirstUsableItem();
		}
		#endregion
	}
}