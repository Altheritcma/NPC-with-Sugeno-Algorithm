using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCTRL : MonoBehaviour
{

	public void LoadScene(string namaScene)
	{
		SceneManager.LoadScene(namaScene);
	}

	public void keluarGame()
	{
		Debug.Log("Keluar Game");
		Application.Quit();
	}
}
