using UnityEngine;

/* An Item that can be equipped to increase armor/damage. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class Equipment : Item {

	public EquipmentSlot equipSlot;     // What slot to equip it in
	public int armorModifier;
	public int damageModifier;

	[SerializeField, Tooltip("Actual item prefab to be instantiated into the game")]
	private GameObject itemPrefab;

	#region Properties

	public GameObject ItemPrefab {
		get { return itemPrefab; }
	}

	#endregion

	// Called when pressed in the inventory
	public override void Use() {
		EquipmentManager.Instance.Equip(this);  // Equip
		RemoveFromInventory();  // Remove from inventory
	}

}

public enum EquipmentSlot { Weapon, Armor }
