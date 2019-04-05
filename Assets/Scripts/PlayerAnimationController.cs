using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : Singleton<PlayerAnimationController> {

	private Animator anim;

	private void Start() {
		anim = GetComponent<Animator>();
	}

	public void Walk(bool state) {
		anim.SetBool("Walk", state);
	}
}
