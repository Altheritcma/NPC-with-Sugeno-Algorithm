using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace AnaCerita
{
	public class ManagerGames : MonoBehaviour {
		
		BossCTRL boss;
		AjiSakaController player;		
		PanelCTRL panelCTRL;
		public GameObject GameBackMusic;
		bool isGameOver, isWin;

//		void Awake()
//		{
//			GameSoundManager.PlayBackSound();
//		}

		void Start()
		{
			boss = GameObject.Find("Boss").GetComponent<BossCTRL> ();
			player = GameObject.Find("AjiSaka").GetComponent<AjiSakaController> ();
			panelCTRL = GameObject.Find("PauseCTRL").GetComponent<PanelCTRL> ();

			GameSoundManager.PlayBackSound();
			isGameOver = false;
			isWin = false;
		}

		void FixedUpdate()
		{
			if (player != null && boss != null)
				return;
			
				if (player == null) {
				Invoke ("Gameover", 1f);
				}

				if (boss == null) {
				Invoke ("Win", 1f);
				}

			if(isGameOver)
			{
				panelCTRL.LoadPanelGameOver ();
			}

			if (isWin) 
			{
				panelCTRL.LoadPanelWin ();
			}

		}


		void Gameover()
		{
			isGameOver = true;
		}

		void Win()
		{
			isWin = true;
		}

//		public static ManagerGames instance = null;
//		[HideInInspector]
//		public bool playerGetMove = true;
//
//		private BossDewatacengkarGagalScript BossSatu;
//		private bool bossSatuMoving = true;
//		private bool doingSetup = true;
//
//		void Awake(){
//			if (instance == null)
//				instance = this;
//			else if (instance != this)
//				Destroy (gameObject);
//				
//			DontDestroyOnLoad (gameObject);
//
//			BossSatu = GameObject.Find("DewataCengkar").GetComponent<BossDewatacengkarGagalScript>();
//
//
//
//		}
//	
//
//		void InitGame (){
//
//			doingSetup = true;
//
//		}
//			
//
//		void Update(){
//			
//			if (playerGetMove || bossSatuMoving)
//			return;
//
//			//BossBehaviour ();
//		}
//
//
//		void BossBehaviour(){
//			bossSatuMoving = true;
//			BossSatu.Move ();
//		}
	}
}
