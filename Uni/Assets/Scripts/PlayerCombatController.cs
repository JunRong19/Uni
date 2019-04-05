using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCombatController : Singleton<PlayerCombatController> {

	[SerializeField, Tooltip("Weapon the player is currently holding.")]
	private Weapon currentWeapon;

	private WeaponType currentWeaponType;

	#region Properties

	public Weapon CurrentWeapon {
		get { return currentWeapon; }
		set {
			currentWeapon = value;
			if(value != null) {
				SetWeaponType();
			}
		}
	}

	#endregion


	private void Update() {
		HandlePlayerAttack();
	}

	void HandlePlayerAttack() {
		if(CurrentWeapon == null) {
			return;
		}
		switch(currentWeaponType) {
			case WeaponType.Melee:
				HandleMeleeAttack();
				break;

			case WeaponType.Ranged:
				HandleRangedAttack();
				break;

			case WeaponType.Hybrid:
				HandleHybridAttack();
				break;
		}
	}

	void HandleMeleeAttack() {
		if(Input.GetMouseButtonDown(0)) {
			Attack();
		}
	}

	void HandleRangedAttack() {
		if(Input.GetMouseButtonDown(0)) {
			Attack();
		}
	}

	void HandleHybridAttack() {
		if(Input.GetMouseButtonDown(0)) {
			Attack();
		}

		if(Input.GetMouseButtonDown(1)) {
			AlternateAttack();
		}
	}

	#region Handle_Attack_Functions

	private void InitializeAttack() {
		LookAtAttackDirection();
	}

	private void Attack() {
		InitializeAttack();
		currentWeapon.Attack();
	}

	private void AlternateAttack() {
		InitializeAttack();
		currentWeapon.AlternateAttack();
	}

	private void LookAtAttackDirection() {
		if(ExtensionMethods.GetWorldPointFromScreen(Input.mousePosition).x >= Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane)).x) {
			transform.localScale = new Vector3(1, 1, 1);
		} else {
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}

	#endregion

	#region Utils

	void SetWeaponType() {
		currentWeaponType = currentWeapon.weaponType;
	}

	public bool IsHoldingWeapon() {
		bool holdingWeapon;
		return holdingWeapon = (CurrentWeapon != null) ? true : false;
	}

	#endregion
}
