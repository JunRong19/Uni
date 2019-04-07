using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EquipmentManager : Singleton<EquipmentManager> {

	public WeaponEquipment defaultWeapon;
	public ArmorEquipment defaultArmor;

	private WeaponEquipment currentWeapon;
	private ArmorEquipment currentArmor;

	[SerializeField, Tooltip("Position of hand")]
	private Transform handPos;

	// Callback for when an item is equipped
	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public event OnEquipmentChanged onEquipmentChanged;

	Inventory inventory;

	void Start() {
		inventory = Inventory.Instance;

		//int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		//currentEquipment = new Equipment[numSlots];

		EquipAllDefault();
	}


	public WeaponEquipment GetWeapon() {
		return currentWeapon;
	}

	public ArmorEquipment GetArmor() {
		return currentArmor;
	}

	void EquipAllDefault() {
		EquipWeapon(defaultWeapon);
		EquipArmor(defaultArmor);
	}

	// Equip a new item
	public void EquipWeapon(WeaponEquipment newItem) {
		Equipment oldItem = null;

		// Find out what slot the item fits in
		// and put it there.
		int slotIndex = (int)newItem.equipSlot;

		// If there was already an item in the slot
		// make sure to put it back in the inventory
		if(currentWeapon != null) {
			oldItem = currentWeapon;

			inventory.Add(oldItem);

			Destroy(handPos.GetChild(0).gameObject);
		}

		// An item has been equipped so we trigger the callback
		if(onEquipmentChanged != null) {
			onEquipmentChanged.Invoke(newItem, oldItem);
		}

		currentWeapon = newItem;
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
		if(currentArmor != null) {
			oldItem = currentArmor;

			inventory.Add(oldItem);
		}

		// An item has been equipped so we trigger the callback
		if(onEquipmentChanged != null) {
			onEquipmentChanged.Invoke(newItem, oldItem);
		}

		currentArmor = newItem;
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
		newWeapon.transform.localScale = Vector2.one;
	}
}
