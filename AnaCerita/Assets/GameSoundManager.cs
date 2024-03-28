using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita

{
	public class GameSoundManager : MonoBehaviour {

		public static AudioClip PlayerAttack, PlayerRun, PlayerDie, PlayerJump, PlayerGetDamage, enemySound1, enemySound2,
		CollectItem, soundTeleport, explosion, win;
		static AudioSource audioSource;
		static AudioSource backSound;


		void Start()
		{
			PlayerAttack = Resources.Load<AudioClip>("scavengers_enemy1");
			PlayerRun = Resources.Load<AudioClip> ("scavengers_footstep2");
			PlayerDie = Resources.Load<AudioClip> ("scavengers_die");
			PlayerJump = Resources.Load<AudioClip> ("Player-jump1");
			PlayerGetDamage = Resources.Load<AudioClip> ("Player-ouch2");
			enemySound1 = Resources.Load<AudioClip> ("scavengers_enemy1");
			enemySound2 = Resources.Load<AudioClip> ("enemy-death2");
			CollectItem = Resources.Load<AudioClip> ("bombCollect");
			soundTeleport = Resources.Load<AudioClip> ("scavengers_soda2");
			explosion = Resources.Load<AudioClip> ("bigBoom");
			win = Resources.Load<AudioClip> ("MainTheme");

			audioSource = GetComponent<AudioSource> ();
			backSound = GetComponent<AudioSource> ();
		}

		public static void PlayBackSound()
		{
			backSound.Play ();
			backSound.loop = true;
		}

		public static void PauseBackSound()
		{
			backSound.Pause ();
		}


		public static void StopBackSound()
		{
			backSound.Stop ();
		}


		public static void PlaySound(string clip)
		{
			switch (clip) {
			case "PlayerAttack":
				audioSource.PlayOneShot (PlayerAttack);
				break;
			}

			switch (clip) {
			case "PlayerRun":
				audioSource.PlayOneShot (PlayerRun);
				break;
			}

			switch (clip) {
			case "PlayerDie":
				audioSource.PlayOneShot (PlayerDie);
				break;
			}

			switch (clip) {
			case "PlayerJump":
				audioSource.PlayOneShot (PlayerJump);
				break;
			}

			switch (clip) {
			case "PlayerGetDamage":
				audioSource.PlayOneShot (PlayerGetDamage);
				break;
			}

			switch (clip) {
			case "EnemyGetDamage":
				audioSource.PlayOneShot (enemySound1);
				break;
			}

			switch (clip) {
			case "EnemyDeath":
				audioSource.PlayOneShot (enemySound2);
				break;
			}

			switch (clip) {
			case "GetItem":
				audioSource.PlayOneShot (CollectItem);
				break;
			}

			switch (clip) {
			case "Teleport":
				audioSource.PlayOneShot (soundTeleport);
				break;
			}

			switch (clip) {
			case "explosion":
				audioSource.PlayOneShot (explosion);
				break;
			}

			switch (clip) {
			case "win":
				audioSource.PlayOneShot (win);
				break;
			}
		}

	}
}