using UnityEngine;

/* An Item that can be equipped to increase armor/damage. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapon")]
public class WeaponEquipment : Equipment {

	[SerializeField, Tooltip("Actual item prefab to be instantiated into the game")]
	private GameObject itemPrefab;

	#region Properties

	public GameObject ItemPrefab {
		get { return itemPrefab; }
	}

	#endregion

	// Called when pressed in the inventory
	public override void Use() {
		EquipmentManager.Instance.EquipWeapon(this);  // Equip
		RemoveFromInventory();  // Remove from inventory
	}

}
