using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInputManager : MonoBehaviour {

	public static GetInputManager instance;

	[SerializeField] protected TouchButtonScript upButton;
	[SerializeField] protected TouchButtonScript leftButton;
	[SerializeField] protected TouchButtonScript rightButton;
	[SerializeField] protected TouchButtonScript downButton;

	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

	}

	public enum ButtonState
	{
		None,
		PressedDown,
		Release,
		Held
	}
		


	public Vector2 CurrentInputHorizontal{
		get
		{
			return new Vector2 (HorizontalInput, 0);	
		}
	}
	public Vector2 CurrentInputVertical
	{
		get
		{
			return new Vector2 (0 ,VerticalInput);
		}
	}

	public float HorizontalInput{
		get{
			if (leftButton.CurrentState == ButtonState.Held || leftButton.CurrentState == ButtonState.PressedDown) {
				
				return -1;
			} else if (rightButton.CurrentState == ButtonState.Held || rightButton.CurrentState == ButtonState.PressedDown) {
				
				return 1;
			}
			return Input.GetAxis ("Horizontal");
		}
	}

	public float VerticalInput{
		get{
			if (upButton.CurrentState == ButtonState.PressedDown) {
				return 1;
			} 
			else if (downButton.CurrentState == ButtonState.PressedDown) {
				return -1;
			}
			return Input.GetAxis ("Vertical");
		}
	}


}
