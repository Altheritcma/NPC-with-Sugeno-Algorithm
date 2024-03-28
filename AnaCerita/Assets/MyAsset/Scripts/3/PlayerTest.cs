using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour {

	GetInputManager inputManager;
	[SerializeField] float playerSpeed = 5f;

	public void Awake()
	{
		inputManager = GetComponent<GetInputManager>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (inputManager.CurrentInputHorizontal * Time.deltaTime * playerSpeed);
	}
}
