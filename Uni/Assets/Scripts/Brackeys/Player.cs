using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* This class just makes it faster to get certain components on the player. */

public class Player : Singleton<Player> {

	void Start() {
		playerStats.OnHealthReachedZero += Die;
	}

	//public CharacterCombat playerCombatManager;
	public PlayerStats playerStats;


	void Die() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public int GetFacingDirection() {
		return (int)Mathf.Sign(transform.localScale.x);
	}
}
