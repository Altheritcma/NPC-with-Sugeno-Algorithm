﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnaCerita
{
	public class MenuCTRL : MonoBehaviour 
	{
		public void LoadScene(string sceneName)
		{
			SceneManager.LoadScene (sceneName);
		}

		public void QuitGame()
		{
			Application.Quit ();
		}
	}
}