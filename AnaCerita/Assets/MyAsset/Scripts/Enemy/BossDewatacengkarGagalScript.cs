using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnaCerita
{
	public class BossDewatacengkarGagalScript : MonoBehaviour {

		[Header ("Stats")]
		public int health;
		public int damage;
		public float cepatJalan;
		private float cepatJalanValue;
		public float jarakBerhenti;
		public float jarakPlayerTerpantau;
		public float waktuMulaiDelayMenembak;
		private float timeMulaiShowtandaTanya = 2f;
		private float timeShowTandaTanya;
		private float waktuDelayMenembak;
		private float DazedTime;
		public float startDazedTime = 2f;
		//private float timeBtwDamage = 1.5f;
		public float m_timerToattack;
		private float m_Time;
		private float timeBeforeStopMove;
		private float StartTimeBeforeStopMove = 1f;
		private float timeEfekTeleport;
		private float StartTimeEfekTeleport = 1f;
		private float timeDelayToTeleportAgain;
		private float timeStartDTTA = 3f;
		public float stopRanges;
		public LayerMask whatIsPoint;
		public LayerMask WhatisEnemies;


		Rigidbody2D rb;

		[Header("Referensi")]
		public GameObject Tembak;
		private Transform player;
		public Transform spawnPoint;
		public Transform StopMove;

		public Animator camAnim;
		public Slider healthBar;

		private Animator anim;
		public bool isDead;
		private bool facingRight = true;
		//private bool isPatrol = true;
		//private bool canAttack = false;
		private bool isGrounding;
		private bool isMove;
		public Transform groundDetection;

		public GameObject efekSpawn;
		public GameObject efekDamage;

		public Transform efekTeleport;
		public Transform efekTeleport2;
		public Transform efekTeleport3;
		public Transform point1;
		public Transform point2;
		public Transform point3;
		public Transform point4;
		public Transform point5;


		// Use this for initialization
		void Start () {
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D>();

			waktuDelayMenembak = 3;
			m_Time = m_timerToattack;
			timeShowTandaTanya = timeMulaiShowtandaTanya;
			cepatJalanValue = cepatJalan;
			timeBeforeStopMove = StartTimeBeforeStopMove;
			timeEfekTeleport = StartTimeEfekTeleport;
			timeDelayToTeleportAgain = timeStartDTTA;


		}

		// Update is called once per frame
		void FixedUpdate ()
		{

//			if (isMove) {
//				
//			}
//
			Collider2D hit = Physics2D.OverlapCircle (transform.position, stopRanges, whatIsPoint);
//			point1.gameObject.SetActive (false);
//			point4.gameObject.SetActive (false);
//			point5.gameObject.SetActive (false);

			//Untuk Mengecek ke Atas jika di atas ada player jalan berhneti
			Debug.DrawRay (transform.position, Vector2.up * 30, Color.yellow);	
			if (Physics2D.Raycast (transform.position, Vector2.up, 30, WhatisEnemies)) {

				MoveToTarget (move: false);
			}

			// jika player mendekat maka akan mundur
			if (Vector2.Distance (transform.position, player.position) <= jarakPlayerTerpantau) {
				if (Vector2.Distance (transform.position, player.position) > jarakBerhenti) {

					transform.eulerAngles = new Vector2 (0, 0);
					isMove = true;
					anim.SetBool ("isWalk", true);

					if (hit && hit.tag == "Point3") {

						anim.SetBool ("isWalk", false);
						isMove = false;
						transform.Translate (new Vector2(0, 0));
						SpawnTuyul (spawn : true);

					} else{

						anim.SetBool ("isWalk", true);
						isMove = true;

					}

				} else if (Vector2.Distance (transform.position, player.position) < jarakBerhenti && Vector2.Distance (transform.position, player.position) > jarakBerhenti - 4) {

					//transform.position = this.transform.position;
					isMove = false;
					anim.SetBool ("isWalk", false);
					transform.eulerAngles = new Vector2 (0, 0);
					SpawnTuyul (spawn: true);

				} else if (Vector2.Distance (transform.position, player.position) < jarakBerhenti - 4) {

					transform.Translate (new Vector2(-cepatJalanValue , 0) * Time.deltaTime);
					transform.eulerAngles = new Vector2 (0, -180);
					anim.SetBool ("isWalk", true);
					SpawnTuyul (spawn: false);
				}
			} 



				if (hit && hit.tag == "Point3") {

					anim.SetBool ("isWalk", false);
					isMove = false;
					transform.Translate (new Vector2(0, 0));
					SpawnTuyul (spawn : true);

				} else{

					anim.SetBool ("isWalk", true);
					isMove = true;

				}
//
//
//			}
//
//
//
//			// Untuk menggerakkan musuh ini ke Player
//			//Untuk menyatakan keputusan dari health musuh ini
//
			if (isMove) {
				Move ();
			}

			if(health <= 75) //jika jumlah nyawa berada di 75 ke bawah
			{


				point1.gameObject.SetActive (true);
				point2.gameObject.SetActive (true);
				transform.eulerAngles = new Vector2 (0, -180);
				isMove = true;
				Move ();



				Collider2D hitpoint1 = Physics2D.OverlapCircle (transform.position, stopRanges, LayerMask.GetMask ("Point1"));
				if (hitpoint1) {
					SpawnTuyul (spawn: false);
					timeBeforeStopMove -= Time.deltaTime;

					if (timeBeforeStopMove <= 0) {
						isMove = false;
						Instantiate (efekSpawn, efekTeleport.position, Quaternion.identity);


					} 
				}


				Collider2D hitpoint2 = Physics2D.OverlapCircle (transform.position, stopRanges, LayerMask.GetMask ("Point2"));
				if (hitpoint2) {

					timeDelayToTeleportAgain -= Time.deltaTime;
					point1.gameObject.SetActive (false);

					anim.SetBool ("isWalk", false);
					isMove = false;
					transform.eulerAngles = new Vector2 (0, 0);
					SpawnTuyul (spawn : true);




				}

//				if (timeDelayToTeleportAgain <= 0) {
//					point1.gameObject.SetActive (false);
//					point4.gameObject.SetActive (true);
//					point5.gameObject.SetActive (true);
//					point2.gameObject.SetActive (false);
//
//					timeBeforeStopMove = StartTimeBeforeStopMove;
//				}



				Collider2D hitpoint3 = Physics2D.OverlapCircle (transform.position, stopRanges, LayerMask.GetMask ("Point4"));
				if (hitpoint3) {


					if (timeBeforeStopMove <= 0) {
						isMove = false;
						Instantiate (efekSpawn, efekTeleport3.position, Quaternion.identity);


					} else {
						timeBeforeStopMove -= Time.deltaTime;
					}

					anim.SetBool ("isWalk", false);
					isMove = false;
					transform.eulerAngles = new Vector2 (0, 0);
					SpawnTuyul (spawn : false);

				}

				Collider2D hitpoint4 = Physics2D.OverlapCircle (transform.position, stopRanges, LayerMask.GetMask ("Point5"));
				if (hitpoint4) {
					anim.SetBool ("isWalk", false);
					isMove = false;
					transform.eulerAngles = new Vector2 (0, 0);
					SpawnTuyul (spawn : true);
				}
			}
			if (health <= 50)
			{

			}
			if (health <= 25) 
			{
				//anim.SetTrigger("stageTwo");
			}

			if (health <= 0) 
			{
				//anim.SetTrigger("death");
			}

			// memberi player jeda waktu untuk menghindar sebelum musuh ini memberi damage lebih
			//		if (timeBtwDamage > 0) {
			//			timeBtwDamage -= Time.deltaTime;
			//		}

			// Untuk mendeklarasikan jumlah float nyawa ke bentuk gamabr bar nyawa
			healthBar.value = health;
		}

		public void TakeDamage(int Damage){
			DazedTime = startDazedTime;
			Instantiate(efekDamage, transform.position, Quaternion.identity);
			health -= Damage;
		}



		public void Move(){
			anim.SetBool ("isWalk", true);
			transform.Translate (Vector2.left * cepatJalanValue * Time.deltaTime);
		}


		public void Patrol(){
			transform.Translate (Vector2.right * cepatJalanValue * Time.deltaTime);
			CheckGround ();

		}

		public void SpawnTuyul(bool spawn){
			if (spawn == true) {
				if (waktuDelayMenembak <= 2) {
					anim.SetBool ("isSpawn", true);

				}
				if (waktuDelayMenembak <= 0) {
					anim.SetBool ("isSpawn", false);
					Instantiate (Tembak, spawnPoint.position, Quaternion.identity);
					Instantiate(efekSpawn, spawnPoint.position, Quaternion.identity);
					waktuDelayMenembak = waktuMulaiDelayMenembak;
				} else {
					waktuDelayMenembak -= Time.deltaTime;
				}
			} else {
				anim.SetBool ("isSpawn", false);
				waktuDelayMenembak = waktuMulaiDelayMenembak;
			}
		}

		public void MoveToPoint1(bool move){
			if (move == true) {
				if (point1.position.x <= transform.position.x) {
					anim.SetBool ("isWalk", true);
					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					transform.Translate (Vector2.left * cepatJalanValue * Time.deltaTime);

					transform.eulerAngles = new Vector2 (0, 0);

					//facingRight = false;

				} else {
					anim.SetBool ("isWalk", true);
					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					transform.Translate (Vector2.left * cepatJalanValue * Time.deltaTime);

					transform.eulerAngles = new Vector2 (0, -180);
					//facingRight = true;
				}
			}

			else{
				transform.Translate (Vector2.zero);
			}

		}


		public void MoveToPoint3( bool move){
			if(move == true){
				if (point3.position.x < transform.position.x) {
					anim.SetBool ("isWalk", true);
					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					//		 	transform.Translate (Vector2.left * cepatJalanValue * Time.deltaTime);
					isMove = false;
					transform.eulerAngles = new Vector2 (0, 0);

					//facingRight = false;

				} else  {
					anim.SetBool ("isWalk", true);
					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					transform.Translate (Vector2.left * cepatJalanValue * Time.deltaTime);

					transform.eulerAngles = new Vector2 (0, -180);
					//facingRight = true;
				}
			}
			else{
				anim.SetBool ("isWalk", false);
				isMove = false;
			}
		}


		public void MoveToTarget(bool move){
			if (move == true) {
				if (player.position.x <= transform.position.x) {
					anim.SetBool ("isWalk", true);
					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					transform.Translate (Vector2.left * cepatJalanValue * Time.deltaTime);

					transform.eulerAngles = new Vector2 (0, 0);

					//facingRight = false;

				} else {
					anim.SetBool ("isWalk", true);
					//rb.position = (Vector2.MoveTowards(transform.position, player.position, cepatJalan * Time.deltaTime));
					transform.Translate (Vector2.left * cepatJalanValue * Time.deltaTime);

					transform.eulerAngles = new Vector2 (0, -180);
					//facingRight = true;
				}
			} else {
				anim.SetBool ("isWalk", false);
				transform.Translate (Vector2.zero);
			}
		}

		public void enemyWaitToAttack(){

			if (m_Time <= 0) {


				m_Time = m_timerToattack; 

			} else {
				Debug.Log ("player");
				m_Time -= Time.deltaTime;

			}
		}
		void Flip(){
			facingRight = !facingRight;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		public void CheckGround(){
			RaycastHit2D groundInfo = Physics2D.Raycast (groundDetection.position, Vector2.down, 2f);
			Debug.DrawRay (groundDetection.position, Vector2.down, Color.red);

			if (groundInfo.collider == false) {
				if (facingRight == true) {
					transform.eulerAngles = new Vector2 (0, -180);
					//Flip();
					facingRight = false;
				} else {
					transform.eulerAngles = new Vector2 (0, 0);
					//Flip();
					facingRight = true;
				}
			}
		}

		void OnDrawGizmosSelected(){
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (StopMove.position, stopRanges);
		}

		bool AttackRangeCheck(){
			if (Vector2.Distance (transform.position, player.position) <= jarakPlayerTerpantau) {
				return true;
			} else {
				return false;
			}
		}

		bool StopRangeCheck(){
			if (Vector2.Distance (transform.position, player.position) <= jarakBerhenti) {
				return true;
			} else {
				return false;
			}

		}
	}
}