using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory> {


	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	[SerializeField, Tooltip("How many items can the inventory hold")]
	private int space;

	// Our current list of items in the inventory
	public List<Item> items = new List<Item>();

	// Add a new item if enough room
	public void Add(Item item) {
		if(item.showInInventory) {
			if(items.Count >= space) {
				Debug.Log("Not enough room.");
				return;
			}

			items.Add(item);

			if(onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}
	}

	// Remove an item
	public void Remove(Item item) {
		items.Remove(item);

		if(onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public void SwapWeapon(Item item) {
		// If player is holding weapon.
		if(PlayerCombatController.Instance.CurrentWeapon != null) {
			// Get the current weapon item object and add it to the inventory.
			Item prevItem = PlayerCombatController.Instance.CurrentWeapon.item;
			Add(prevItem);
		}
		// Remove weapon that is being swap in the inventory and equip it.
		Remove(item);
		PlayerCombatController.Instance.EquipWeapon(item);
	}

	public void SwapArmor(Item item) {
		// Handle armor swaping
	}

}
