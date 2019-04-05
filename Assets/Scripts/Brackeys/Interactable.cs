using UnityEngine;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Player")) {
			// Show pick up ui (X to pick up)
		}
	}

}
