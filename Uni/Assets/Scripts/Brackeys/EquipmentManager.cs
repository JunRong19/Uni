using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EquipmentManager : Singleton<EquipmentManager> {

	public Equipment[] defaultWear;

	private Equipment[] currentEquipment;

	[SerializeField, Tooltip("Position of hand")]
	private Transform handPos;

	// Callback for when an item is equipped
	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public event OnEquipmentChanged onEquipmentChanged;

	Inventory inventory;

	void Start() {
		inventory = Inventory.Instance;

		int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];

		EquipAllDefault();
	}


	public Equipment GetEquipment(EquipmentSlot slot) {
		return currentEquipment[(int)slot];
	}

	void EquipAllDefault() {
		foreach(ArmorEquipment a in defaultWear) {
			EquipArmor(a);
		}
	}

	// Equip a new item
	public void EquipWeapon(WeaponEquipment newItem) {
		Equipment oldItem = null;

		// Find out what slot the item fits in
		// and put it there.
		int slotIndex = (int)newItem.equipSlot;

		// If there was already an item in the slot
		// make sure to put it back in the inventory
		if(currentEquipment[slotIndex] != null) {
			oldItem = currentEquipment[slotIndex];

			inventory.Add(oldItem);

			Destroy(handPos.GetChild(0).gameObject);
		}

		// An item has been equipped so we trigger the callback
		if(onEquipmentChanged != null) {
			onEquipmentChanged.Invoke(newItem, oldItem);
		}

		currentEquipment[slotIndex] = newItem;
		Debug.Log(newItem.name + " equipped!");

		if(newItem.ItemPrefab) {
			InitializeWeapon(newItem);
		}
	}

	public void EquipArmor(ArmorEquipment newItem) {
		Equipment oldItem = null;

		// Find out what slot the item fits in
		// and put it there.
		int slotIndex = (int)newItem.equipSlot;

		// If there was already an item in the slot
		// make sure to put it back in the inventory
		if(currentEquipment[slotIndex] != null) {
			oldItem = currentEquipment[slotIndex];

			inventory.Add(oldItem);
		}

		// An item has been equipped so we trigger the callback
		if(onEquipmentChanged != null) {
			onEquipmentChanged.Invoke(newItem, oldItem);
		}

		currentEquipment[slotIndex] = newItem;
		Debug.Log(newItem.name + " equipped!");
		InitializeArmor(newItem);

	}

	void InitializeWeapon(WeaponEquipment item) {
		PlayerCombatController.Instance.CurrentWeapon = item.ItemPrefab.GetComponent<Weapon>();
		GameObject newWeapon = Instantiate(item.ItemPrefab, transform.position, Quaternion.identity);
		HandleWeaponSpawnPos(ref newWeapon);
	}

	void InitializeArmor(ArmorEquipment item) {
		PlayerAnimationController.Instance.SetArmorLayerWeight(item.skinLayer);
	}

	void HandleWeaponSpawnPos(ref GameObject newWeapon) {
		newWeapon.transform.rotation = Quaternion.identity;
		newWeapon.transform.parent = handPos;
		newWeapon.transform.localPosition = Vector2.zero;
	}
}
