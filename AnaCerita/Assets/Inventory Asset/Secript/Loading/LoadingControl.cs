using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingControl : MonoBehaviour {

	public Transform LoadingBar;
	public string MainMenuScane;

	[SerializeField]
	private float currentAmount;
	[SerializeField]
	private float speed;

	void Update () 
	{
		if (currentAmount < 100) 
		{
			currentAmount += speed * Time.deltaTime;
			Debug.Log ((int)currentAmount);
		}
		else
		{
			SceneManager.LoadScene (MainMenuScane);
    	}
		LoadingBar.GetComponent<Image> ().fillAmount = currentAmount / 100;

}
}
