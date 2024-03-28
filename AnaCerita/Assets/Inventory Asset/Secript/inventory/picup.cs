using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class picup : MonoBehaviour {

	private inventoryControl inventory;
	public GameObject itembutton;

	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<inventoryControl> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			for (int i = 0; i < inventory.slots.Length; i++) 
			{
				if (inventory.isfull [i] == false) 
				{
					// item can added to inventory 
					inventory.isfull[i] = true;
					Instantiate(itembutton, inventory.slots[i].transform, false);
					Destroy (gameObject);
					break;
				}
			
			}
		}
	}
}
