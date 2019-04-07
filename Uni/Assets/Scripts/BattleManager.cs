using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

	public GameObject battleCanvas;

	public GameObject player;
	public GameObject enemy;

	private Animator playerAnim;
	private Animator enemyAnim;

	private WeaponEquipment currentWeapon;
	private ArmorEquipment currentArmor;

	[SerializeField, Tooltip("Sprite of weapon Uni is holding.")]
	private Image weaponSprite;

	private enum TurnOrder { player, enemy }

	private TurnOrder currentTurn;

	private PlayerStats playerStats;

	#region Properties

	#endregion

	private void Start() {
		battleCanvas.SetActive(false);
		playerStats = Player.Instance.playerStats;
	}

	public void InitializeBattle() {
		battleCanvas.SetActive(true);
		GetEquipment();
		InitializePlayer();
		currentTurn = TurnOrder.player;
		StartCoroutine(EnemyPrepareAttack());
		//Debug.Log(playerStats.currentHealth);
		//Debug.Log(playerStats.maxHealth.GetValue());
		//Debug.Log(playerStats.damage.GetValue());
		//Debug.Log(playerStats.armor.GetValue());
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
		weaponSprite.sprite = item.icon;
	}

	public void PlayerAttack() {
		if(currentTurn == TurnOrder.player) {
			playerAnim.SetTrigger("Attack");
			EndTurn(currentTurn);
		}
	}

	void EndTurn(TurnOrder previousTurn) {
		CheckForDeath();

		currentTurn = (previousTurn == TurnOrder.player) ? TurnOrder.enemy : TurnOrder.player;
	}

	IEnumerator EnemyPrepareAttack() {
		yield return new WaitUntil(() => currentTurn == TurnOrder.enemy);

		//enemyAnim.SetTrigger("Attack");
		EndTurn(currentTurn);
		StartCoroutine(EnemyPrepareAttack());
	}

	void CheckForDeath() {

	}
}
