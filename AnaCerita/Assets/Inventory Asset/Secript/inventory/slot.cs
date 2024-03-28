using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot : MonoBehaviour
{
	private inventoryControl inventory;
	public int i;

	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag ("Player").GetComponent<inventoryControl> ();
	}

	private void Update()
	{
		if (transform.childCount <= 0) 
		{
			inventory.isfull [i] = false;
		}
	}

    public void DropItem() {
        foreach (Transform child in transform) {
			child.GetComponent <spawn>().SpawnDroppedItem ();
			GameObject.Destroy(child.gameObject);
        }
    }
}
