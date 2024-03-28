using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCTRL : MonoBehaviour {

	private InventoryCTRL inventory;
	public GameObject itemButton;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<InventoryCTRL> ();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			for (int i = 0; i < inventory.slots.Length; i++)
			{
				if (inventory.isFull [i] == false) {
					inventory.isFull [i] = true;
					Instantiate (itemButton, inventory.slots[i].transform, false);
					Destroy (gameObject);
					break;
				}
			}
		}
	}
}
