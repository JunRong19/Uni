using UnityEngine;

/* An Item that can be equipped to increase armor/damage. */

public class Equipment : Item {

	public EquipmentSlot equipSlot;     // What slot to equip it in

	public int armorModifier;
	public int damageModifier;

	// Called when pressed in the inventory
	public override void Use() {

	}

}