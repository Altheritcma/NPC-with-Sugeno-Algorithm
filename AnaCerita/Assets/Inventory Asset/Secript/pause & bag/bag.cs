using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag : MonoBehaviour {

	public GameObject inventorymenu, inventorybutton, btn_ctrl;

	public void  inventory () 
	{
		inventorymenu.SetActive (true);
		inventorybutton.SetActive (false);
		btn_ctrl.gameObject.SetActive (false);
		Time.timeScale = 0;
	}

	public void inventoryresume () 
	{
		inventorymenu.SetActive (false);
		inventorybutton.SetActive (true);
		btn_ctrl.gameObject.SetActive (true);
		Time.timeScale = 1;
	}
}
