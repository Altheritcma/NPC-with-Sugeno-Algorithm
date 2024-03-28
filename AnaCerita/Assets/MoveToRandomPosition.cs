using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnaCerita
{
	public class MoveToRandomPosition : MonoBehaviour {
		
		Vector2 whereToRandPosX;
		int randomPosX;
		float randomSpawnPosX, timeForJumpBack, spawnRate, nextSpawn = 0.0f;
		public Transform Object, efekteleportPoint;
		public Transform[] spawnPoint;
		public GameObject enemy, efekTeleport;

		void Start()
		{
			timeForJumpBack = 0.5f;
			spawnRate = Random.Range (2f, 6f);
		}


		public void MoveToRandomPos()
		{
			timeForJumpBack -= Time.deltaTime;

			if (timeForJumpBack <= 0) {

				timeForJumpBack = 0.5f;
				Object.position = transform.position;
			}


			if (timeForJumpBack >= 0) {

//				randomPos = Random.Range (-8.4f, 20f);

				Instantiate (efekTeleport, efekteleportPoint.position, Quaternion.identity);
			}
		}

		public void SpawnRandom()
		{
			if (Time.time > nextSpawn) 
			{
				nextSpawn = Time.time + spawnRate;
				randomPosX = Random.Range (0, spawnPoint.Length);
				Instantiate (enemy, spawnPoint [randomPosX].position, Quaternion.identity);
			}
		}
	}
}
