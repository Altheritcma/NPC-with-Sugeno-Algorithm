using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class PanelCTRL : MonoBehaviour {

		public Transform kontrolPlayer, panelPause, panelGameOver, panelWin, btn_ctrl;

		void Start()
		{
			panelPause.gameObject.SetActive (false);
			panelGameOver.gameObject.SetActive (false);

			panelWin.gameObject.SetActive (false);
			Time.timeScale = 1f;
		}
			


		public void LoadPanelWin()
		{
			Time.timeScale = 0f;

			panelWin.gameObject.SetActive (true);

			kontrolPlayer.gameObject.SetActive (false);

			GameSoundManager.StopBackSound ();

			GameSoundManager.PlaySound ("win");


		}

		public void LoadPanelPause()
		{
			Time.timeScale = 0f;

			panelPause.gameObject.SetActive (true);

			btn_ctrl.gameObject.SetActive (false);

			kontrolPlayer.gameObject.SetActive (false);

			GameSoundManager.PauseBackSound ();
		}


		public void LoadPanelGameOver()
		{
			Time.timeScale = 0f;

			panelGameOver.gameObject.SetActive (true);

			kontrolPlayer.gameObject.SetActive (false);

			GameSoundManager.StopBackSound ();

			GameSoundManager.PlaySound ("PlayerDie");
		}


		public void UnPause()
		{
			Time.timeScale = 1f;

			panelPause.gameObject.SetActive (false);

			btn_ctrl.gameObject.SetActive (true);

			kontrolPlayer.gameObject.SetActive (true);

			GameSoundManager.PlayBackSound();
		}


		public void UnLoadPanelGameOver()
		{
			Time.timeScale = 1f;

			panelGameOver.gameObject.SetActive (false);

			kontrolPlayer.gameObject.SetActive (true);

			GameSoundManager.PlayBackSound();
		}

		public void UnLoadPanelWin()
		{
			Time.timeScale = 1f;

			panelWin.gameObject.SetActive (false);

			kontrolPlayer.gameObject.SetActive (true);

			GameSoundManager.PlayBackSound();
		}

	}
}
