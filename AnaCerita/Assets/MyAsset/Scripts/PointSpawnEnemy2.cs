﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AnaCerita
{
	public class PointSpawnEnemy2 : MonoBehaviour {

		[Header("Referensi")]
		public Transform enemyPrefab;
		public Transform pointSpawn;
		public float spawnRate = 0.25f;

		[Header("Other")]
		private float spawnCooldown;
		Animator anim;


		void Awake()
		{
			anim = GetComponent<Animator> ();
		}

		// Use this for initialization
		void Start () 
		{
			spawnCooldown = 0f;
		}

		// Update is called once per frame
		void Update () 
		{
			if (spawnCooldown > 0) 
			{
				spawnCooldown -= Time.deltaTime;
			}
		}


		public void Spawn(bool isSpawn)
		{
			if (CanSpawn) 
			{
				spawnCooldown = spawnRate;


				Instantiate (enemyPrefab, pointSpawn.position, Quaternion.identity);

			}
		}

		public bool CanSpawn
		{
			get
			{
				return spawnCooldown <= 0f;
			}
		}
	}
}
