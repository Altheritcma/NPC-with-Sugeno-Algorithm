using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetect : MonoBehaviour {

	public bool wallDetect;

	BoxCollider2D boxColl;
	CharacterController CTRL;


	void Start () 
	{

		CTRL = GameObject.Find("AjiSaka").GetComponent<CharacterController> ();
		boxColl = GetComponent<BoxCollider2D> ();
	}

	void Update()
	{
		if (wallDetect) {

			boxColl.isTrigger = false;
		}

	}




}
