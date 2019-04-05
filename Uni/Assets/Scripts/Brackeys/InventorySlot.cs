using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour {

	public Image icon;

	Item item;  // Current item in the slot

	// Add item to the slot
	public void AddItem(Item newItem) {
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
	}

	// Clear the slot
	public void ClearSlot() {
		item = null;

		icon.sprite = null;
		icon.enabled = false;
	}

	// If the remove button is pressed, this function will be called.
	public void RemoveItemFromInventory() {
		Inventory.Instance.Remove(item);
	}

	// Use the item
	public void UseItem() {
		HandleItemUse();
	}

	private void HandleItemUse() {
		if(item == null) {
			return;
		}
		switch(item.ItemType) {
			case ItemType.Weapon:
				Inventory.Instance.SwapWeapon(item);
				break;

			case ItemType.Armor:
				Inventory.Instance.SwapArmor(item);
				break;

			case ItemType.Consumable:
				item.Use();
				break;
		}
	}

}
