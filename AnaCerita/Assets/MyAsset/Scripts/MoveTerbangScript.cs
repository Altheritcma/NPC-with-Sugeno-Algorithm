using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class MoveTerbangScript : MonoBehaviour {
		
		// 0 - Designer variables

		/// <summary>
		/// Projectile speed
		/// </summary>
		public Vector2 speed = new Vector2(5, 5);

		/// <summary>
		/// Moving direction
		/// </summary>
		public Vector2 direction = new Vector2(-1, 0);

		private Vector2 movement;

		void Update()
		{
			// 1 - Movement
			movement = new Vector2(
				speed.x * direction.x,
				speed.y * direction.y);
		}

		void FixedUpdate()
		{
			// Apply movement to the rigidbody
			GetComponent<Rigidbody2D>().velocity = movement;
		}
	}
}

