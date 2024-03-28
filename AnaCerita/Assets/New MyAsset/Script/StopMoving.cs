using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class StopMoving : MonoBehaviour {

		public enum TriggerType{Enter, Exit};

//		[SerializeField] Transform teleportTo;
		[SerializeField] string tag;
		[SerializeField] TriggerType type;

		CharacterController boss;

		void Start()
		{
			boss = GameObject.Find ("Boss").GetComponent<CharacterController> ();
		}

		void OnTriggerStay2D (Collider2D other)
		{
			if (type != TriggerType.Enter)
				return;

			if (tag == string.Empty || other.CompareTag (tag)) {
				boss.StopMoving ();
			}

			
		}


		void OnTriggerExit2D (Collider2D other)
		{
			if (type != TriggerType.Exit)
				return;

			if (tag == string.Empty || other.CompareTag (tag)) {
			
				boss.StopMoving ();
			}
		}

	}	
}