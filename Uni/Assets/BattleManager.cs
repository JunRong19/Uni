using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

	public GameObject battleCanvas;

	public GameObject player;
	public GameObject enemy;

	private Animator playerAnim;
	private Animator enemyAnim;

	private WeaponEquipment currentWeapon;
	private ArmorEquipment currentArmor;

	[SerializeField, Tooltip("Position of hand")]
	private Transform handPos;

	private void Start() {
		battleCanvas.SetActive(false);
	}

	public void InitializeBattle() {
		battleCanvas.SetActive(true);
		GetEquipment();
		InitializePlayer();
	}

	private void GetEquipment() {
		currentWeapon = EquipmentManager.Instance.GetWeapon();
		currentArmor = EquipmentManager.Instance.GetArmor();
	}

	private void InitializePlayer() {
		playerAnim = player.GetComponent<Animator>();
		InitializePlayerEquipment();
	}

	void InitializePlayerEquipment() {
		Debug.Log(currentArmor.skinLayer);
		SetArmorLayerWeight(currentArmor.skinLayer);
		InitializeWeapon(currentWeapon);
	}

	private void InitializeEnemy() {
		enemyAnim = enemy.GetComponent<Animator>();
	}

	private void SetArmorLayerWeight(int i) {
		for(int n = 1; n < playerAnim.layerCount + 1; n++) {
			int val = ((n) == i) ? 1 : 0;
			playerAnim.SetLayerWeight(n, val);
		}
	}


	void InitializeWeapon(WeaponEquipment item) {
		GameObject newWeapon = Instantiate(item.ItemPrefab, transform.position, Quaternion.identity);
		HandleWeaponSpawnPos(ref newWeapon);
	}

	void HandleWeaponSpawnPos(ref GameObject newWeapon) {
		newWeapon.transform.rotation = Quaternion.identity;
		newWeapon.transform.parent = handPos;
		newWeapon.transform.localPosition = Vector2.zero;
		newWeapon.transform.localScale = Vector2.one;
	}
}
