using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {

	/// <summary>
	/// Get world position from mouse position.
	/// </summary>
	/// <param name="screenPos">Mouse position</param>
	/// <returns></returns>
	public static Vector2 GetWorldPointFromScreen(Vector2 screenPos) {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(screenPos);
		mousePos.z = 0;
		return mousePos;
	}
}
