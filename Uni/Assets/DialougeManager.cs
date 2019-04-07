using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialougeManager : Singleton<DialougeManager> {
	[SerializeField]
	private TextMeshProUGUI dialougeText;

	[SerializeField]
	private TextMeshProUGUI[] optionText = new TextMeshProUGUI[2];

	public enum OptionSelection { left, right }

	public OptionSelection selectedOption;

	private string defaultLeft, defaultRight;

	public UnityEvent selectedChoice = new UnityEvent();

	private bool selecting;

	private void Start() {
		ClearDialouge();
		ToggleOption(false);
	}

	public IEnumerator PlayDialogue(string[] dialouge, string[] options) {
		Debug.Log(dialouge.Length);
		for(int i = 0; i < dialouge.Length; i++) {
			if(i >= dialouge.Length - 1 && options != null) {
				HandleOptionSelection(options);
			}
			yield return StartCoroutine(AnimateText(dialouge[i]));
		}
	}

	IEnumerator AnimateText(string strComplete) {
		int i = 0;
		ClearDialouge();
		while(i < strComplete.Length) {
			dialougeText.text += strComplete[i++];
			yield return new WaitForSeconds(0.05f);
		}
		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
	}

	void HandleOptionSelection(string[] options) {
		if(options.Length > 2) {
			Debug.Log("There are more than 2 choices!");
			return;
		}
		ToggleOption(true);
		for(int i = 0; i < options.Length; i++) {
			optionText[i].text = options[i];
		}

		defaultLeft = optionText[0].text;
		defaultRight = optionText[1].text;
		selecting = true;
	}

	private void Update() {
		if(selecting) {
			if(Input.GetKeyDown(KeyCode.A)) {
				optionText[1].text = defaultRight;
				selectedOption = OptionSelection.left;
				optionText[0].text = "> " + defaultLeft;
			}
			if(Input.GetKeyDown(KeyCode.D)) {
				optionText[0].text = defaultLeft;
				selectedOption = OptionSelection.right;
				optionText[1].text = "> " + defaultRight;
			}

			if(Input.GetKeyDown(KeyCode.Return)) {
				selecting = false;
				selectedChoice.Invoke();
			}
		}
	}

	private void ClearDialouge() {
		dialougeText.text = "";
	}

	private void ToggleOption(bool state) {
		for(int i = 0; i < optionText.Length; i++) {
			optionText[i].enabled = state;
		}
	}
}
