using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

	[SerializeField, Tooltip("What type of weapon is this?")]
	public WeaponType weaponType;

	public abstract void Attack();

	public virtual void AlternateAttack() { }

}
