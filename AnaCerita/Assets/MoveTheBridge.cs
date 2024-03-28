using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class MoveTheBridge : MonoBehaviour {

		public float speedY;
		private float delayForNextMove;
		private Rigidbody2D rb;
		private float speed;
		public Transform item, posA, posB, Btn_Lift_Up, Btn_Lift_Down;
		public bool isMoveDown, isMoveUp;

		// Use this for initialization
		void Start () 
		{
			rb = GetComponent<Rigidbody2D> ();

//			Btn_Lift_Down = GameObject.Find("Btn_lift").GetComponentInChildren<Transform> ();
//
//			Btn_Lift_Up = GameObject.Find("Btn_lift").GetComponentInChildren<Transform> ();

//			posA = childTransform.localPosition;
//			posB = transformB.localPosition;
//			nextPos = posB;
		}

		// Update is called once per frame
		void Update () {

			Collider2D hit = Physics2D.OverlapCircle (transform.position, 1f, LayerMask.GetMask("Player"));

			if (item == null && hit && Vector2.Distance (transform.position, posA.position) < 0.1f) {
				Btn_Lift_Down.gameObject.SetActive (true);
				isMoveUp = false;
			}

			if (item == null && !hit && Vector2.Distance (transform.position, posA.position) > 0.1f || item == null && !hit && Vector2.Distance (transform.position, posB.position) > 0.1f || 
				item != null && !hit && Vector2.Distance (transform.position, posA.position) > 0.1f || item != null && !hit && Vector2.Distance (transform.position, posB.position) > 0.1f) 
			{
				Btn_Lift_Down.gameObject.SetActive (false);
				Btn_Lift_Up.gameObject.SetActive (false);
			}

			if (item == null && hit && Vector2.Distance (transform.position, posB.position) < 0.1f) {
				Btn_Lift_Up.gameObject.SetActive (true);
				isMoveDown = false;
			}


			if (isMoveDown) {
			
				transform.position = Vector3.MoveTowards(transform.position, posB.position, speedY * Time.deltaTime);
			}

			if (isMoveUp) {

				transform.position = Vector3.MoveTowards(transform.position, posA.position, speedY * Time.deltaTime);
			}
		}


		void Moving(float speedY)
		{
			rb.velocity = Vector2.up * speed;
		}

		void Move(float speedY)
		{
			transform.Translate(Vector2.up * speedY * Time.deltaTime);
		}

		void ChangeMove()
		{
			


		}

		public void MoveUp()
		{
//			speed = speedY;
//			posB.gameObject.SetActive (false);
//			if (Vector2.Distance(transform.position, posA.position) < 0.1 ) {
//
//				StopMoving ();
//			}

			isMoveUp = true;
		}

		public void MoveDown()
		{
//			speed = -speedY;
//			posA.gameObject.SetActive (false);
//			if (Vector2.Distance(transform.position, posB.position) < 0.1 ) {
//				StopMoving ();
//			}

			isMoveDown = true;
		}

		void StopMoving()
		{
			speed = 0;
		}

	}
}
