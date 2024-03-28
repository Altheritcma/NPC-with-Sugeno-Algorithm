using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class DoorTeleport : MonoBehaviour {

//		public enum TriggerType{Enter, Exit};
//
		[SerializeField] Transform teleportTo;
		[SerializeField] string tag;
//		[SerializeField] TriggerType type;
		[SerializeField] Transform efekTeleport, pointEfekTeleport, pointEfekTeleport2, enemies;
		[SerializeField] LayerMask whatIsEnemy;


		[Header("-------Other-------")]
		private float timeBeforeGoTeleport = 3f;
		public bool isCoolDown;


		CharacterController ajisaka;

		void Start()
		{
			ajisaka = GameObject.Find ("AjiSaka").GetComponent<CharacterController> ();
		}

		void Update()
		{

			Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask("Player"));

			if(hit){

				isCoolDown = true;

				}

				if (isCoolDown) {

					timeBeforeGoTeleport -= Time.deltaTime;


				if (timeBeforeGoTeleport > 0) {

					isCoolDown = true;

					Instantiate (efekTeleport, pointEfekTeleport.position, Quaternion.identity);

					Instantiate (efekTeleport, pointEfekTeleport2.position, Quaternion.identity);

					ajisaka.StopMoving ();
				}

				if (timeBeforeGoTeleport < 1.5f) {

					enemies = GameObject.FindGameObjectWithTag (tag).transform;

					enemies.position = teleportTo.position;


				}


				if (timeBeforeGoTeleport < 0) {
					if (enemies != null) {
						if (enemies.position == teleportTo.position) {
							Destroy (teleportTo.gameObject);
							Destroy (gameObject);
							GameSoundManager.PlaySound ("Teleport");
						}
					}

					timeBeforeGoTeleport = 3f;

					isCoolDown = false;
				}
			}
		}
	}
}
