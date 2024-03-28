using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class HintScript : MonoBehaviour {

		DialogSystem dialog;
		CharacterController player;

		void Start()
		{
			dialog = GameObject.Find ("ManagerGames").GetComponent<DialogSystem> ();
			player = GameObject.Find ("AjiSaka").GetComponent<CharacterController> ();
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "Player") {
				player.StopMoving ();
				dialog.LoadDialogPanel ();
				Destroy (gameObject);
			}
		}
	}
}
