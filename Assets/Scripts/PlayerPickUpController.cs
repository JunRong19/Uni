using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerPickUpController : Singleton<PlayerPickUpController> {

	private void OnTriggerStay2D(Collider2D collision) {
		if(collision.GetComponent<ItemPickup>() && Input.GetKeyDown(KeyCode.X)) {
			collision.GetComponent<ItemPickup>().PickUp();
		}
	}

	#region Ignore

	//[SerializeField, Tooltip("Position of player's hand.")]
	//private Transform handPos;

	//private void OnTriggerEnter2D(Collider2D collision) {
	//	if(collision.CompareTag("Weapon")) {
	//		if(IsHoldingObject) {
	//			return;
	//		}
	//		HandleWeaponPickUp(collision.gameObject);
	//	}
	//}

	//void HandleWeaponPickUp(GameObject weapon) {
	//	//weapon.GetComponent<Collider2D>().isTrigger = true;
	//	Weapon pickedUpWeapon = weapon.GetComponent<Weapon>();
	//	//Debug.Log(pickedUpWeapon);
	//	IsHoldingObject = true;

	//	InitializePickedUpWeapon(pickedUpWeapon);
	//	SetWeaponPickUpPos(weapon);
	//}

	//void InitializePickedUpWeapon(Weapon pickedUpWeapon) {
	//	PlayerCombatController.Instance.CurrentWeapon = pickedUpWeapon;
	//	pickedUpWeapon.ToggleWeaponPhysics(false);
	//}

	//void SetWeaponPickUpPos(GameObject weapon) {
	//	weapon.transform.rotation = Quaternion.identity;
	//	weapon.transform.parent = handPos.transform;
	//	weapon.transform.localPosition = Vector2.zero;
	//}

	#endregion
}
