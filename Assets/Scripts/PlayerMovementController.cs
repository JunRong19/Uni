using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), (typeof(PlayerAnimationController)))]
public class PlayerMovementController : Singleton<PlayerMovementController> {

	[Header("Player's move properties")]

	[SerializeField, Tooltip("Speed of movement.")]
	private float moveSpeed;

	[Header("Player's jump properties")]

	[SerializeField, Tooltip("Affects how high the player jumps.")]
	private float jumpForce;

	[SerializeField, Tooltip("How many times can the player jump")]
	private float maxJumps;

	[SerializeField, Tooltip("How fast the player falls")]
	private float fallMultiplier;

	[SerializeField, Tooltip("Multiplier for low jump")]
	private float lowJumpMultiplier;

	private float jumps;

	private Rigidbody2D rb;

	#region Properties

	#endregion

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}
	void Update() {
		HandleMovement();
		HandleJump();
	}

	void HandleMovement() {
		float horizontal = Input.GetAxisRaw("Horizontal");

		HandleFacingDirection(horizontal);

		rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

		if(horizontal > 0 || horizontal < 0) {
			PlayerAnimationController.Instance.Walk(true);
		} else {
			PlayerAnimationController.Instance.Walk(false);
		}
	}

	void HandleJump() {
		HandleRealisticJump();
		if(Input.GetKeyDown(KeyCode.Space)) {
			if(jumps <= 0) {
				return;
			}
			rb.AddForce(new Vector2(0, jumpForce + GetFallingVelocity()), ForceMode2D.Impulse);
			jumps--;
		}
	}

	void HandleRealisticJump() {

		if(rb.velocity.y < 0) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		} else if(rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space)) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}

	void HandleFacingDirection(float horizontal) {
		if(horizontal > 0) {
			transform.localScale = new Vector3(1, 1, 1);
		} else if(horizontal < 0) {
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}

	#region Utils

	float GetFallingVelocity() {
		if(rb.velocity.y < 0) {
			return Mathf.Abs(rb.velocity.y);
		}
		return 0;
	}


	#endregion

	#region Collisions

	private void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.CompareTag("Ground")) {
			jumps = maxJumps;
		}
	}

	#endregion
}
