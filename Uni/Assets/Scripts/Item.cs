using UnityEngine;

public class Item : ScriptableObject {

	new public string name = "New Item";    // Name of the item
	public Sprite icon = null;              // Item icon
	public bool showInInventory = true;

	[SerializeField, Tooltip("What type of item this is")]
	private ItemType itemType;

	#region Properties

	public ItemType ItemType {
		get { return itemType; }
	}

	#endregion

	// Called when the item is pressed in the inventory
	public virtual void Use() {
		// Use the item
		// Something may happen
	}

	// Call this method to remove the item from inventory
	public void RemoveFromInventory() {
		Inventory.Instance.Remove(this);
	}
}
