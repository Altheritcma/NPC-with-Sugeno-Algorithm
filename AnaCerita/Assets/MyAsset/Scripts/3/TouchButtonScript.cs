using UnityEngine;
using UnityEngine.UI;

public class TouchButtonScript : MonoBehaviour {

	bool PressedDown;
	bool PressedLastFrame;

	public GetInputManager.ButtonState CurrentState;

	public void PressDown(){
		PressedDown = true;
	}

	public void Release()
	{
		PressedDown = false;
	}

	private void Update()
	{
		if (PressedDown) {
			if (PressedLastFrame) {
				CurrentState = GetInputManager.ButtonState.Held;
			} else {
				CurrentState = GetInputManager.ButtonState.PressedDown;
			}
		} else {
			if (PressedLastFrame) {
				CurrentState = GetInputManager.ButtonState.Release;
			} else {
				CurrentState = GetInputManager.ButtonState.None;
			}
		}
	}

	private void LateUpdate()
	{
		PressedLastFrame = PressedDown;
	}
}
