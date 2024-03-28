using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class HealthMiniBar : MonoBehaviour {
		Vector3 localScale;
		BanaspatiBossScript banaspati;

		void Start()
		{
			localScale = transform.localScale;
			banaspati = GameObject.Find ("BanaspatiBoss").GetComponent<BanaspatiBossScript> ();
		}

		void Update()
		{
			localScale.x = banaspati.health;
			transform.localScale = localScale;
		}
	}
}
