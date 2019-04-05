using UnityEngine;

public class ItemPickup : Interactable {

	public Item item;   // Item to put in the inventory if picked up

	// Pick up the item
	public void PickUp() {
		Debug.Log("Picking up " + item.name);
		Inventory.Instance.Add(item);   // Add to inventory

		Destroy(gameObject);    // Destroy item from scene
	}

}
