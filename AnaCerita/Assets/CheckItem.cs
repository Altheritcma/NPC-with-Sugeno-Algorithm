using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckItem : MonoBehaviour {

	public Transform item, target;

	void Update()
	{
		if (item != null) {

			if (target == null) {
				item.gameObject.SetActive (true);
			}
		}

		if (item == null) {
			Destroy (gameObject);
		}
	}
}
