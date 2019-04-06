using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : Singleton<PlayerAnimationController> {

	private Animator anim;

	private void Start() {
		anim = GetComponent<Animator>();
	}

	void SetArmorLayerWeight(int i) {
		for(int n = 1; n < anim.layerCount + 1; n++) {
			int val = ((n) == i) ? 1 : 0;
			anim.SetLayerWeight(n, val);
		}
	}

	public void Walk(bool state) {
		anim.SetBool("Walk", state);
	}
}
