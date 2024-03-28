using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class itemScript : MonoBehaviour {
		public GameObject efekColl;

		void Start()
		{
			transform.gameObject.SetActive (false);
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.tag == "Player") {
				Destroy (gameObject);
				Instantiate (efekColl, transform.position, Quaternion.identity);
				GameSoundManager.PlaySound ("GetItem");
			}
		}
	}
}
