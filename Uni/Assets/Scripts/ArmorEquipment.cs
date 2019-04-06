using UnityEngine;

/* An Item that can be equipped to increase armor/damage. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Armor")]
public class ArmorEquipment : Equipment {

	public int skinLayer;

	// Called when pressed in the inventory
	public override void Use() {
		EquipmentManager.Instance.EquipArmor(this);  // Equip
		RemoveFromInventory();  // Remove from inventory
	}

}
