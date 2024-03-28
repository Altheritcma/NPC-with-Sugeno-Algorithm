using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class CheckGround : MonoBehaviour {

		public bool isGrounded;


		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.tag == "Ground") {
				isGrounded = true;
			}
		}
	}
}