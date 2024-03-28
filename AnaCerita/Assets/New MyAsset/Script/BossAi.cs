using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class BossAi : MonoBehaviour {

		//yang akan di tampilkan di hierarchi.

		[Header("Status & Variable Sets")] 

		public bool isMoving, hasSpawn, isSpawnis, Teleport, isGoTeleport;
		public int health, damage;
		public float speed, jarakDekat, jarakSedang, jarakJauh, minAttackCooldown = 0.5f, maxAttackCooldown = 2f, timeEfekTeleport = 2f;


		[Header("Misc GameObject")]

		[SerializeField] Slider healthBar; 
		[SerializeField] GameObject efekDamage;
		[SerializeField] GameObject efekTeleport;
		[SerializeField] Transform efekTeleportTransform;
		[SerializeField] Transform point1, point2, point3, point4, point5;
		[SerializeField] Transform Destination_1, Destination_2;

		//Script & Fungsi yang di panggil

		[Header("Referensi")]

		Rigidbody2D rb;
		Animator anim;
		PointSpawnEnemy pointSpawn;
		PointSpawnEnemy2 pointSpawn2;
		SpriteRenderer SP;
		private float aiCooldown, aiCooldown2;
		private Transform Player, banaspati;


		//Semua Variable & Referensi akan di deklarasi kan di bawah ini 

		void Awake() // Referensi di atas akan di panggil sebelum suatu proses akan di mulai/Start().
		{
			anim = GetComponent<Animator> ();
			rb = GetComponent<Rigidbody2D> ();
			SP = GetComponent<SpriteRenderer> ();

		}


		void Start() // yang akan di proses di awal
		{
			
		}


		void Update() // ini berbeda sedikit dengan FixedUpdate(), di proses setiap frame
		{
			
		}


		void FixedUpdate() // kalo yang ini selalu terproses per frame perdetik, yah lupa2 ingat nih wkwk
		{
			
		}


		//sebuah fungsi yang akan di deklarasikan ke fixedupdate/update()

	}
}

