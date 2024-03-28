using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	[RequireComponent(typeof(Collider2D))]

	public class TeleportScript : MonoBehaviour {

		public enum TriggerType{Enter, Exit};

		[SerializeField] Transform teleportTo;
		[SerializeField] string tag;
		[SerializeField] TriggerType type;
		[SerializeField] Transform efekTeleport;
		[SerializeField] Transform pointEfekTeleport;
		[SerializeField] LayerMask whatIsEnemy;
		[SerializeField] Transform enemies;


		[Header("-------Other-------")]
		private float timeBeforeGoTeleport = 2f;
		bool isCoolDown;


		void Update()
		{
			Collider2D hit = Physics2D.OverlapCircle (transform.position, 2f, whatIsEnemy);

			if (hit) {
				
				if (timeBeforeGoTeleport > 0) {

					isCoolDown = true;

					Instantiate (efekTeleport, pointEfekTeleport.position, Quaternion.identity);
				}

				if (timeBeforeGoTeleport < 0) {

					enemies = GameObject.FindGameObjectWithTag ("Boss").transform;

					enemies.position = teleportTo.position;

				}



			} else {
			
				timeBeforeGoTeleport = 1;

				isCoolDown = false;

			}


			if (isCoolDown) {

				timeBeforeGoTeleport -= Time.deltaTime;

			}
			if (enemies != null) {
				if (enemies.position == teleportTo.position) {
					transform.position = this.transform.position;
				}
			}
		}



//		void OntriggerStay2D(Collider2D other)
//		{
//			if (type != TriggerType.Enter)
//				return;
//
//			if (tag == string.Empty || other.CompareTag (tag)) {
//			
//			}
//		}


//		void OnTriggerEnter2D (Collider2D other)
//		{
//			if (type != TriggerType.Enter)
//				return;
//
//			if (tag == string.Empty || other.CompareTag (tag))
//			
//				isCoolDown = true;
//
//			Instantiate (efekTeleport, transform.position, Quaternion.identity);
//
//			if (timeBeforeGoTeleport <= 0) {
//				isCoolDown = false;
//				other.transform.position = teleportTo.position;
//			}
//		}
//
//
//		void OnTriggerExit2D (Collider2D other)
//		{
//			if (type != TriggerType.Exit)
//				return;
//
//			if (tag == string.Empty || other.CompareTag (tag))
//				other.transform.position = teleportTo.position;
//		}

	}
}
