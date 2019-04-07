using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeTrigger : MonoBehaviour {
	[SerializeField, TextArea(1, 4)]
	string[] dialouge;

	[SerializeField, Tooltip("Can the player make a choice?")]
	private string[] optionChoices;

	private bool hasTriggered;

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("Player")) {
			if(hasTriggered) {
				return;
			}
			if(optionChoices != null) {
				DialougeManager.Instance.selectedChoice.AddListener(GetChoiceResult);
			}
			StartCoroutine(DialougeManager.Instance.PlayDialogue(dialouge, optionChoices));
			hasTriggered = true;
		}
	}

	private void GetChoiceResult() {
		Debug.Log(DialougeManager.Instance.selectedOption);
	}
}
