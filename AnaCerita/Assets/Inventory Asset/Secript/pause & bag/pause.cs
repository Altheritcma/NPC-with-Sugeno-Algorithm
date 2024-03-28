using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {

	public GameObject pausemenu, pausebutton;

	public void  pausee () 
	{
		pausemenu.SetActive (true);
		pausebutton.SetActive (false);
		Time.timeScale = 0;
	}

	public void resume () 
	{
		pausemenu.SetActive (false);
		pausebutton.SetActive (true);
		Time.timeScale = 1;
	}
}
