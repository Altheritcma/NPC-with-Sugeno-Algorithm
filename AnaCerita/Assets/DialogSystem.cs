using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class DialogSystem : MonoBehaviour {

		public GameObject dialogPanel, button;


		void Start () {

			dialogPanel.gameObject.SetActive (false);

//			Time.timeScale = 1f;
		}

		public void LoadDialogPanel()
		{
			button.gameObject.SetActive (false);
			dialogPanel.gameObject.SetActive (true);
//			Time.timeScale = 0f;
		}

		public void UnLoadDialogPanel()
		{
			button.gameObject.SetActive (true);
			dialogPanel.gameObject.SetActive (false);

//			Time.timeScale = 1f;
		}

	}
}
